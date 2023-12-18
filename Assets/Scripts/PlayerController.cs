using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.WebCam;

/**
 * The goal is to collect 100 points, ORIGINAL OR PREVIOUS PLAN
 * Player tasks
 * 1. Player can move left-right and jump
 * 2. Player can have collisions with automatically spawned objects
 *   a. Prizes => collects points
 *   b. Barriers => death
 * 3. Player cannot move out of bounds or jump again before landing
 * 4. Player can make boost jump through using points
 * 5. Collided prizes disappear
 * 6. Not collided prizes disappear after moving out of bounds
 *
 * Plan has developed, CURRENT
 * 1. Player can move left-right & jump - low and high
 * - jumps A. High: space and k B. Low: l (small L)
 * 2. Spawned barriers
 * - spawn in random x-positions, y and z is constant
 * - size is random in range
 * - speed is random in range
 * - interval is changing randomly in range
 * - collision subtracts points, amount depends on game level
 * 3. Spawned prizes
 * - spawn in random x-positions and y-positions in range, z is constant
 * - size is random in range
 * - speed is random in range
 * - interval is changing randomly in range
 * - hit value is random in range, 4 values, different colour
 * - missed prizes subtract points, amount depends on game level
 * 4. Points
 * - player can have minus points, the limit depends on game level
 * - player loses when points fall below the limit 
 * - winning points depend on game level
 * 5. The result is time, either
 * - the playing time before losing or
 * - the playing time until gaining winning points
 * - at least one playing session statistics/records will be shown
 */
public class PlayerController : MonoBehaviour {
    public int points;
    public float jumpForce;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public bool gameOver = false;
    public float speed = 100;
    /*
     * Game levels 0, 1, 2, 3, 4
     * Barrier collision
        * 0 -> -1 point, 1 -> -1 point, 2 -> -2 points, 3 -> -2 points, 4 -> -3 points
     * Missed prize
        * 0 -> -1 point, 1 -> -2 points, 2 -> -2 points, 3 -> -3 points, 4 -> -3 points
     * Game over, lost game
        * 0 -> -12 points, 1 -> -11 points, 2 -> -10 points, 3 -> -9 points, 4 -> -8 points   
     */
    public int gameLevel = 0;
    public int[] minusBarrierCollisions = { -1, -1, -2, -2, -3 };
    public int[] minusMissedPrizes = { -1, -2, -2, -3, -3 };
    public int[] minusLostGame = { -12, -11, -10, -9, -8 };
    //private float leftLimit = -5.5f;
    //private float rightLimit = 3f;
    private float playerPosZ = -4f;
    private Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start() {
        playerRb = gameObject.GetComponent<Rigidbody>();
        Debug.Log("speed=" + speed);
    }

    // Update is called once per frame
    void Update() {

        if(!gameOver) {
            if (gameObject.transform.position.z < playerPosZ) {
                transform.Translate(Vector3.forward * 0.10f);
//Debug.Log("newZ=" + gameObject.transform.position.z);                
            }
            float horizontalInput = Input.GetAxis("Horizontal");        
            if(horizontalInput != 0.0f) {
                // Debug.Log("horizontalInput: " + horizontalInput + ", force: " + Vector3.right * speed * horizontalInput);
                playerRb.AddForce(Vector3.right * speed * horizontalInput);
/*                if (gameObject.transform.position.x < leftLimit ||
                    gameObject.transform.position.x > rightLimit) {
                       // playerRb.AddForce(Vector3.right * speed * -horizontalInput);
                }
*/                
            }
            if(Input.GetKeyDown(KeyCode.Space) && isOnGround) {
                isOnGround = false;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
//Debug.Log("1 OnCollisionEnter: " + other.gameObject.name);        
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }else if (other.gameObject.CompareTag("Barrier")) {
            points += minusBarrierCollisions[gameLevel]; //1;
            Debug.Log("Points=" + points +", " + minusBarrierCollisions[gameLevel] +" points lost");
            if (points < minusLostGame[gameLevel]) { //-2) {
                gameOver = true;
                Debug.Log("Game over");
            }else {
                    
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
Debug.Log("OnTriggerEnter");        
        if (other.gameObject.CompareTag("Prize")) {
            int objPoints = other.gameObject.GetComponent<MoveDown>().points; 
            points += objPoints;
Debug.Log("is Prize, points=" + objPoints + "totPoints: " + points);
            Destroy(other.gameObject);
        }
    }
}

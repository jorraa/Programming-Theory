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
    //public int points;
    [SerializeField] float jumpForce;
    [SerializeField] public float gravityModifier = 1;
    [SerializeField]  bool isOnGround = true;
    public bool gameOver = false;
    [SerializeField]  float speed = 100;
    private float playerPosZ = -4f;
    private Rigidbody playerRb;
    private GameLevelController gameLevelController;

    private GameObject scoreText;
    // Start is called before the first frame update
    void Start() {
        playerRb = gameObject.GetComponent<Rigidbody>();
        gameLevelController = GameObject.Find("GameLevel").gameObject.GetComponent<GameLevelController>();
        scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update() {
        if(!GameManager.Instance.gameOver) {
            // Barrier may push player in front of it.
            // Player has to move back, means go through the barrier 
            if (gameObject.transform.position.z < playerPosZ) {
                transform.Translate(Vector3.forward * 0.10f);
            }
            float horizontalInput = Input.GetAxis("Horizontal");        
            if(horizontalInput != 0.0f) {
                MovePlayer(horizontalInput); // ABSTRACTION
            }
            if(Input.GetKeyDown(KeyCode.Space)) {
                JumpPlayer(); // ABSTRACTION
            }
        }
    }

    private void MovePlayer(float force) {
        playerRb.AddForce(Vector3.right * speed * force);
    }

    private void JumpPlayer() {
        if(isOnGround) {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision other) {
        HandleCollision(other); // ABSTRACTION
    }

    private void OnTriggerEnter(Collider other) {
        HandleCollision(other); // ABSTRACTION
        
    }

    // Polymorphism - overloading HandleCollision
    private void HandleCollision(Collision other) { // POLYMORPHISM
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }else if (other.gameObject.CompareTag("Barrier")) {
            GameManager.Instance.UpdateScore(gameLevelController.GetBarrierCollisionPoints());
        }
    }

    private void HandleCollision(Collider other) { // POLYMORPHISM
        if (other.gameObject.CompareTag("Prize")) {
            int objPoints = other.gameObject.GetComponent<PrizeController>().points; 
            GameManager.Instance.points += objPoints;
            Debug.Log("is Prize" + other.gameObject.name + ", points=" + objPoints + "totPoints: " + GameManager.Instance.points);
            Destroy(other.gameObject);
        }
    }
}

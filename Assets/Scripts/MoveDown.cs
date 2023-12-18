using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
    public float speed =3;
    public int points;
    private bool gameOver;
    private float zBound = -8;

    private GameObject player;
    private PlayerController playerController;
    private GameLevelController gameLevelController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
        
        gameLevelController = GameObject.Find("GameLevel").gameObject.GetComponent<GameLevelController>();
    }

    // Update is called once per frame
    void Update() {
        gameOver = playerController.gameOver;
        if(!gameOver) {
            transform.Translate(Vector3.back * Time.deltaTime);
            if (transform.position.z < zBound) {
                Destroy(gameObject);
                if (gameObject.CompareTag("Prize")){
                    CalcMissedPrizePoints();
                }
            }
            
            // Check possible GameOver reached
            if (playerController.points < gameLevelController.GetLostGamePoints()){
                playerController.gameOver = true;
                Debug.Log("Game over");
            }
        }
    }

    /*
     * player loses point(s) when prize is missed
     */
    private void CalcMissedPrizePoints() {
         {
            int minusPoints = gameLevelController.GetMissedPrizePoints(); // playerController.minusMissedPrizes[playerController.gameLevel];
            playerController.points += minusPoints;
            Debug.Log("One prize destroyed, player loses " + minusPoints + " points, points now " + 
                      playerController.points);
         }
    }
}

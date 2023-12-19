using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
    public int points;
    private bool gameOver;
    private float zBound = -8;
    private GameLevelController gameLevelController;

    void Start()
    {
        gameLevelController = GameObject.Find("GameLevel").gameObject.GetComponent<GameLevelController>();
    }

    void Update() {
        gameOver = GameManager.Instance.gameOver;
        if(!gameOver) {
            if (transform.position.z < zBound) {
                Destroy(gameObject);
                if (gameObject.CompareTag("Prize")){
                    CalcMissedPrizePoints();
                }
            }
        }
    }

    /*
     * player loses point(s) when prize is missed
     */
    private void CalcMissedPrizePoints() {
        int minusPoints = gameLevelController.GetMissedPrizePoints();
        GameManager.Instance.UpdateScore(minusPoints);
        Debug.Log("One prize destroyed, player loses " + minusPoints + " points, points now " + 
                  GameManager.Instance.points);
    }
}

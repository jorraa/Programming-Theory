using System.Collections;
using System.Collections.Generic;
using Prizes;
using UnityEngine;

public class PrizeController : MonoBehaviour {
    [SerializeField] Prize prize;
    public int points = 0; 
    private GameLevelController gameLevelController;
    void Start(){
        SetPrize();
        gameLevelController = GameManager.Instance.gameLevelController;
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.Instance.gameOver) {
            prize.Move(gameObject.transform);
            if (transform.position.z < GameManager.Instance.zBound) {
                Destroy(gameObject);
                if (gameObject.CompareTag("Prize")){
                    CalcMissedPrizePoints();
                }
            }
        }
    }

    void SetPrize() {
        prize = new Prizes.HardPrize();
    }

    /*
     * player loses point(s) when prize is missed
     */
    private void CalcMissedPrizePoints() {
       int minusPoints = gameLevelController.GetMissedPrizePoints(); // playerController.minusMissedPrizes[playerController.gameLevel];
       GameManager.Instance.UpdateScore(minusPoints);
       /* Debug.Log("One prize destroyed, player loses " + minusPoints + " points, points now " + 
                  GameManager.Instance.points);*/
    }

}

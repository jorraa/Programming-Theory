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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        gameOver = playerController.gameOver;
        if(!gameOver) {
            transform.Translate(Vector3.back * Time.deltaTime);
            if (transform.position.z < zBound) {
                if (gameObject.CompareTag("Prize")) {
                    // player loses one point when prize is missed
                    int minusPoints = playerController.minusMissedPrizes[playerController.gameLevel];
                    playerController.points += minusPoints;
                    Debug.Log("One prize destroyed, player loses " + minusPoints + " points, points now " + 
                              playerController.points);
                }
                Destroy(gameObject);
            }
        }
    }
}

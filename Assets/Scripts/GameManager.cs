using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameLevelController gameLevelController;
    public int points;
    public bool gameOver;
    public float zBound = -8;
    public float leftLimitX = -3;
    public float rightLimitX = 2;
    public float upLimitY = 3f;
    public float downLimitY = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameLevelController = GameObject.Find("GameLevel").gameObject.GetComponent<GameLevelController>();
    }
    
    public void GameOver() {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }
    private void Start() { }

    public void UpdateScore(int scoreToAdd) {
        points += scoreToAdd;
        scoreText.text = "Score: " + points;
        if (points < gameLevelController.GetLostGamePoints()){
            GameOver();
        }
    }}

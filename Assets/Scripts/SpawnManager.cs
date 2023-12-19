using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject barrier;
    public GameObject[] prizes;
    
    private float startDelay = 1.5f;

    private float barrierSpawnInterval;
    private float barrierSpawnIntervalMin = 2.5f;
    private float barrierSpawnIntervalMax = 4f;
    
    private float prizeSpawnInterval;
    private float prizeSpawnIntervalMin = 2f;
    private float prizeSpawnIntervalMax = 3.0f;

    // prize size
    private static float prizeWidthMin = 0.3f;
    private static float prizeWidthMax = 1f;
    private static float prizeHeightMin = 0.2f;
    private static float prizeHeightMax = 2f;
    private static float prizeDepth = 0.3f; // prizeZ size
    
    private static float limitScaleX = 2f;
    private static float limitLeftX = -4f;
    private static float limitRightX = 2.8f;
    private static float gameareaWidth = limitRightX - limitLeftX - limitScaleX; // (2* half) //7f;

    // prize position
    private static float prizePosMinX = limitLeftX; // + limitScaleX/4;
    private static float prizePosMaxX = limitRightX; // - limitScaleX/4;

    private static float prizePosMinY = 0.2f;
    private static float prizePosMaxY = 3f;
    private float spawnPosZ = 4;
    
    private GameObject player;

    private bool gameOver;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        barrierSpawnInterval = Random.Range(barrierSpawnIntervalMin, barrierSpawnIntervalMax);
        prizeSpawnInterval = Random.Range(prizeSpawnIntervalMin, prizeSpawnIntervalMax);
        //InvokeRepeating("SpawnRandomBarrier", startDelay, barrierSpawnInterval); 
        InvokeRepeating("SpawnRandomPrize", startDelay, prizeSpawnInterval);
    }

    // Update is called once per frame
    void Update() {
        gameOver = GameManager.Instance.gameOver;
    }

    //No barriers running in OOP submission
    void SpawnRandomBarrier() {
        if(!gameOver) {
            float barrierHeight = Random.Range(1f, 3f);
            float barrierWidth = Random.Range(1.5f, 6f);
            float barrierPosX = Random.Range(-6f, gameareaWidth - barrierWidth);
            barrier.gameObject.transform.position = new Vector3(barrierPosX, 0.05f, spawnPosZ);
            barrier.transform.localScale = new Vector3(barrierWidth, barrierHeight, 0.1f);
            Instantiate(barrier);
        }
    }

    void SpawnRandomPrize() {
        if (!gameOver) {
            int prizeNbr = Random.Range(0, 4);
            GameObject prize = gameObject.GetComponent<SpawnManager>().prizes[prizeNbr];
            float prizeWidth = Random.Range(prizeWidthMin, prizeWidthMax);
            float prizeHeight = Random.Range(prizeHeightMin, prizeHeightMax);
            float prizePosX = Random.Range(prizePosMinX, prizePosMaxX);
            float prizePosY = Random.Range(prizePosMinY, prizePosMaxY);
            
            prize.gameObject.transform.position = new Vector3(prizePosX, prizePosY, spawnPosZ);
            prize.gameObject.transform.localScale = new Vector3(prizeWidth, prizeHeight, prizeDepth);
            Instantiate(prize);
        }
    }
}

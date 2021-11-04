using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] obstaclePatterns;
    private float timeBetweenSpawn;
    public float startTimeBetweenSpawn;
    public float decreaseTime;
    public float minTime = 0.65f;
    private GameObject previous;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        spawnPatternOnGameScreen();
    }

    private void spawnPatternOnGameScreen()
    {
        if(timeBetweenSpawn <= 0) {
            if(previous) {
                Destroy(previous.gameObject);
            }
            previous = Instantiate(obstaclePatterns[getRandomPattern()], transform.position, Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn;
            if(startTimeBetweenSpawn > minTime) {
                startTimeBetweenSpawn -= decreaseTime;
            }
        } else {
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    private int getRandomPattern()
    {
        return Random.Range(0, obstaclePatterns.Length);
    }
}

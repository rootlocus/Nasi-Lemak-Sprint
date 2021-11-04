using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public string score = "999999999";
    public Text scoreText;

    void Start()
    {
        score = "0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score;
    }

    public void addScore(int points) {
        int currentScore = int.Parse(score);
        currentScore += points;
        score = currentScore.ToString();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private int score;

    [SerializeField] Text scoreText;

    void Update()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.text = "SCORE:" + score.ToString("0000000");
        PlayerPrefs.SetInt("SCORE", score);
    }
}

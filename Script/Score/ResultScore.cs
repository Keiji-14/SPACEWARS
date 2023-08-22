using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    private int rankingNum = 10;
    private int score;
    private int[] rankingScore;
    [SerializeField] Text resultText;

    // リザルトでランキングにスコアを登録
    void Start()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        resultText.text = "Score : " + score.ToString("0000000");
        
        // 現在のランキングスコアを取得する
        rankingScore = new int[rankingNum];
        for (int i = 0; i < rankingNum; i++)
        {
            rankingScore[i] = PlayerPrefs.GetInt(("RANKINGSCORE" + (i + 1)), 0);
        }

        // リザルトのスコアが10位のスコアを上回ったら
        if (rankingScore[9] < score)
        {
            rankingScore[9] = score;
        }

        for (int i = 0; i < rankingScore.Length; i++)
        {
            for (int j = i; j < rankingScore.Length; j++)
            {
                if (rankingScore[i] < rankingScore[j])
                {
                    int tmp = rankingScore[i];
                    rankingScore[i] = rankingScore[j];
                    rankingScore[j] = tmp;
                }
            }
        }    
    }

    void Update()
    {
        for (int i = 0; i < rankingNum; i++)
        {
            PlayerPrefs.SetInt(("RANKINGSCORE" + (i + 1)), rankingScore[i]);
        }

        // 変更した値を保存
        PlayerPrefs.Save();
    }
}

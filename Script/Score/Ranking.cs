using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    private int rankingNum = 10;
    private int[] rankingScore;

    [Header("ScoreText")]
    [SerializeField] Text[] rankingText;

    [Header("SE")]
    [SerializeField] AudioClip decisionSE;

    [Header("Scene")]
    public ScenesManager scenesManager;

    AudioSource audioSource;

    void Start()
    {
        //Component���擾
        audioSource = GetComponent<AudioSource>();

        // ���݂̃����L���O�X�R�A���擾����
        rankingScore = new int[rankingNum];
        for (int i = 0; i < rankingNum; i++)
        {
            rankingScore[i] = PlayerPrefs.GetInt(("RANKINGSCORE" + (i + 1)), 0);
            rankingText[i].text = rankingScore[i].ToString("0000000");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            audioSource.PlayOneShot(decisionSE);
            scenesManager.TitleScene();
        }
    }
}

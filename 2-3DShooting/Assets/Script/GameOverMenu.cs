using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private float gameOverMenuNum = 0;
    private float canvasX = 960.0f;
    private float canvasY = 540.0f;

    [SerializeField] GameObject selectCursor;

    [Header("SE")]
    [SerializeField] AudioClip cursorSE;
    [SerializeField] AudioClip decisionSE;

    [Header("Scene")]
    public ScenesManager scenesManager;

    AudioSource audioSource;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 上キーかWキーで一つ上にカーソル移動 
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            gameOverMenuNum -= 1;
            audioSource.PlayOneShot(cursorSE);
        }

        // 下キーかSキーで一つ下にカーソル移動 
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            gameOverMenuNum += 1;
            audioSource.PlayOneShot(cursorSE);
        }

        // カーソルが一周する為の処理
        if (gameOverMenuNum > 1)
        {
            gameOverMenuNum = 0;
        }
        if (gameOverMenuNum < 0)
        {
            gameOverMenuNum = 1;
        }

        switch (gameOverMenuNum)
        {
            case 0:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 150.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(decisionSE);
                    scenesManager.GameScene1();
                }
                break;
            case 1:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 250.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(decisionSE);
                    scenesManager.TitleScene();
                }
                break;
        }
    }
}

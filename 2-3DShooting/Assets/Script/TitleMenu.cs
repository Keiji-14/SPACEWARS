using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    private float titleMenuNum;
    private float canvasX = 960.0f;
    private float canvasY = 540.0f;

    private bool openManual = false;
    private bool openOption = false;

    [SerializeField] GameObject titleSelect;
    [SerializeField] GameObject manualMenu;
    [SerializeField] GameObject optionMenu;
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
        // オプションが開いていない場合にカーソルが動くようにする
        if (!openOption && !openManual)
        {
            TitleCursor();
        }
    }

    private void TitleCursor()
    {
        // 上キーかWキーで一つ上にカーソル移動 
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            titleMenuNum -= 1;
            audioSource.PlayOneShot(cursorSE);
        }

        // 下キーかSキーで一つ下にカーソル移動 
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            titleMenuNum += 1;
            audioSource.PlayOneShot(cursorSE);
        }

        // カーソルが一周する為の処理
        if (titleMenuNum > 4)
        {
            titleMenuNum = 0;
        }
        if (titleMenuNum < 0)
        {
            titleMenuNum = 4;
        }

        switch (titleMenuNum)
        {
            case 0:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 80.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(decisionSE);
                    scenesManager.GameScene1();
                }
                break;
            case 1:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 160.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(decisionSE);
                    scenesManager.RankingScene();
                }
                break;
            case 2:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 240.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(cursorSE);
                    if (!openManual)
                    {
                        OpenManual();
                    }
                    else
                    {
                        CloseManual();
                    }
                }
                break;
            case 3:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 320.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    audioSource.PlayOneShot(cursorSE);
                    OpenOption();
                }
                break;
            case 4:
                selectCursor.transform.position = new Vector3(canvasX, canvasY - 400.0f, 0.0f);
                if (Input.GetKeyDown("space"))
                {
                    scenesManager.GameExit();
                }
                break;
        }
    }

    // ゲームの説明画面を表示する処理
    public void OpenManual()
    {
        openManual = true;
        titleSelect.SetActive(false);
        manualMenu.SetActive(true);

        // 説明画面のページを最初に戻す
        PlayerPrefs.SetInt("MANUALPAGE", 1);
    }

    // ゲームの説明画面を閉じる処理
    public void CloseManual()
    {
        openManual = false;
        titleSelect.SetActive(true);
        manualMenu.SetActive(false);
        audioSource.PlayOneShot(cursorSE);
    }

    // ゲームのオプションを開く処理
    public void OpenOption()
    {
        if (!openOption)
        {
            openOption = true;
            titleSelect.SetActive(false);
            optionMenu.SetActive(true); 
        }
    }

    // ゲームのオプションを閉じる処理
    public void CloseOption()
    {
        openOption = false;
        titleSelect.SetActive(true);
        optionMenu.SetActive(false);
        audioSource.PlayOneShot(cursorSE);
    }
}

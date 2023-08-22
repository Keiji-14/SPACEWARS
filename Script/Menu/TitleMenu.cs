using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [Header("TITLE")]
    [SerializeField] GameObject titleSelect;
    [Header("MANUAL")]
    [SerializeField] GameObject manualMenu;
    [Header("OPTION")]
    [SerializeField] GameObject optionMenu;

    [Header("SE")]
    [SerializeField] AudioClip cursorSE;
    [SerializeField] AudioClip decisionSE;

    [Header("Scene")]
    public ScenesManager scenesManager;

    // ゲームの説明画面を表示する処理
    public void OpenManual()
    {
        //openManual = true;
        titleSelect.SetActive(false);
        manualMenu.SetActive(true);

        // 説明画面のページを最初に戻す
        PlayerPrefs.SetInt("MANUALPAGE", 1);
    }

    // ゲームの説明画面を閉じる処理
    public void CloseManual()
    {
        titleSelect.SetActive(true);
        manualMenu.SetActive(false);
    }

    // ゲームのオプションを開く処理
    public void OpenOption()
    {
        titleSelect.SetActive(false);
        optionMenu.SetActive(true); 
    }

    // ゲームのオプションを閉じる処理
    public void CloseOption()
    {
        titleSelect.SetActive(true);
        optionMenu.SetActive(false);
    }
}

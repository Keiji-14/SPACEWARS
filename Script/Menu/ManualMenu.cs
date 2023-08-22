using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualMenu : MonoBehaviour
{
    private int pageNum;

    [SerializeField] Text pageCountText;

    [Header("ArrowButton")]
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    [Header("PageCanvas")]
    [SerializeField] GameObject[] manualPage;

    [Header("SE")]
    [SerializeField] AudioClip cursorSE;

    private enum Page
    {
        Page1 = 1,
        Page2 = 2,
        Page3 = 3,
    }

    void Update()
    {
        pageNum = PlayerPrefs.GetInt("MANUALPAGE", 0);

        pageCountText.text = pageNum.ToString("0") + " / 3";

        switch (pageNum)
        {
            case (int)Page.Page1:
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);
                manualPage[0].SetActive(true);
                manualPage[1].SetActive(false);
                manualPage[2].SetActive(false); 
                break;
            case (int)Page.Page2:
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
                manualPage[0].SetActive(false);
                manualPage[1].SetActive(true);
                manualPage[2].SetActive(false);
                break;
            case (int)Page.Page3:
                leftArrow.SetActive(true);
                rightArrow.SetActive(false);
                manualPage[0].SetActive(false);
                manualPage[1].SetActive(false);
                manualPage[2].SetActive(true);
                break;
        }
    }

    // 左の矢印のボタンを押したときの処理
    public void LeftArrowButton()
    {
        // 最初のページ以外だった場合は前のページに移せる
        if (pageNum > 1)
        {
            pageNum -= 1;

            PlayerPrefs.SetInt("MANUALPAGE", pageNum);
        }
    }

    // 右の矢印のボタンを押したときの処理
    public void RightArrowButton()
    {
        // 最後のページ以外だった場合は次のページに移せる
        if (pageNum < 3)
        {
            pageNum += 1;

            PlayerPrefs.SetInt("MANUALPAGE", pageNum);
        }
    }
}

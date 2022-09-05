using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	private float pauseSelectNum;
	private float canvasX = 960.0f;
	private float canvasY = 540.0f;

	public bool pauseNow = false;
	public bool optionNow = false;

	[SerializeField] GameObject pauseMenu;
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
		// ポーズ画面かオプション画面じゃなかった場合にEscキーを入力でポーズ画面を表示
		if (Input.GetKeyDown("escape"))
		{
			if (!pauseNow && !optionNow)
			{
				pauseSelectNum = 0;
				pauseNow = true;
				pauseMenu.SetActive(true);
				Time.timeScale = 0.0f;
				audioSource.PlayOneShot(cursorSE);
			}
			else
			{
				pauseNow = false;
				optionNow = false;
				pauseMenu.SetActive(false);
				optionMenu.SetActive(false);
				Time.timeScale = 1.0f;
				audioSource.PlayOneShot(decisionSE);
			}
		}

		if (pauseNow)
        {
			PauseCursor();
		}
	}

	private void PauseCursor()
    {
		// 上キーかWキーで一つ上にカーソル移動 
		if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
		{
			pauseSelectNum -= 1;
			audioSource.PlayOneShot(cursorSE);
		}

		// 下キーかSキーで一つ下にカーソル移動 
		if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
		{
			pauseSelectNum += 1;
			audioSource.PlayOneShot(cursorSE);
		}

		// カーソルが一周する為の処理
		if (pauseSelectNum > 2)
		{
			pauseSelectNum = 0;
		}
		if (pauseSelectNum < 0)
		{
			pauseSelectNum = 2;
		}

		switch (pauseSelectNum)
		{
			case 0:
				selectCursor.transform.position = new Vector3(canvasX, canvasY + 120.0f, 0.0f);
				if (Input.GetKeyDown("space"))
				{
					audioSource.PlayOneShot(decisionSE);
					GameBack();
				}			
				break;
			case 1:
				selectCursor.transform.position = new Vector3(canvasX, canvasY - 0.0f, 0.0f);
				if (Input.GetKeyDown("space"))
				{
					OpenOption();
				}
				break;
			case 2:
				selectCursor.transform.position = new Vector3(canvasX, canvasY - 120.0f, 0.0f);
				if (Input.GetKeyDown("space"))
				{
					audioSource.PlayOneShot(decisionSE);
					scenesManager.TitleScene();
				}
				break;
		}
	}

	public void GameBack()
	{
		
		pauseSelectNum = 0;
		pauseNow = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	// ゲームのオプションを開く処理
	public void OpenOption()
	{
		if (!optionNow)
		{
			pauseNow = false;
			optionNow = true;
			pauseMenu.SetActive(false);
			optionMenu.SetActive(true);
			audioSource.PlayOneShot(cursorSE);
		}
	}

	// ゲームのオプションを閉じる処理
	public void CloseOption()
	{
		pauseNow = true;
		optionNow = false;
		pauseMenu.SetActive(true);
		optionMenu.SetActive(false);
		audioSource.PlayOneShot(cursorSE);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	public bool pauseNow = false;
	public bool optionNow = false;

	[Header("Pause")]
	[SerializeField] GameObject pauseMenu;
	[Header("Option")]
	[SerializeField] GameObject optionMenu;

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
		pauseMenu.SetActive(false);
	}

	void Update()
	{
		// ポーズ画面かオプション画面じゃなかった場合にEscキーを入力でポーズ画面を表示
		if (Input.GetKeyDown("escape"))
		{
			if (!pauseNow && !optionNow)
			{
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
	}

	// ゲームに戻る処理
	public void GameBack()
	{
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
		}
	}

	// ゲームのオプションを閉じる処理
	public void CloseOption()
	{
		pauseNow = true;
		optionNow = false;
		pauseMenu.SetActive(true);
		optionMenu.SetActive(false);
	}
}

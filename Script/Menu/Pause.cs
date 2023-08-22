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
		//Component���擾
		audioSource = GetComponent<AudioSource>();
		pauseMenu.SetActive(false);
	}

	void Update()
	{
		// �|�[�Y��ʂ��I�v�V������ʂ���Ȃ������ꍇ��Esc�L�[����͂Ń|�[�Y��ʂ�\��
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

	// �Q�[���ɖ߂鏈��
	public void GameBack()
	{
		pauseNow = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	// �Q�[���̃I�v�V�������J������
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

	// �Q�[���̃I�v�V��������鏈��
	public void CloseOption()
	{
		pauseNow = true;
		optionNow = false;
		pauseMenu.SetActive(true);
		optionMenu.SetActive(false);
	}
}

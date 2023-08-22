using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // �X�e�[�W��X�R�A�̏�ԁA�v���C���[�̗̑͂����Z�b�g���Ă���Q�[�����(�X�e�[�W1)�ɑJ�ڂ���R���[�`���Ăяo��
    public void GameScene1()
    {
        PlayerPrefs.SetInt("PLAYERHP", 5);
        PlayerPrefs.DeleteKey("PHASE");
        PlayerPrefs.DeleteKey("SCORE");
        PlayerPrefs.DeleteKey("STAGE");
        StartCoroutine(ChangeGameScene1());
    }

    // �Q�[�����(�X�e�[�W1)�ɑJ��
    private IEnumerator ChangeGameScene1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameScene1");
    }

    // �Q�[�����(�X�e�[�W2)�ɑJ��
    public void GameScene2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    // �Q�[�����(�X�e�[�W3)�ɑJ��
    public void GameScene3()
    {
        SceneManager.LoadScene("GameScene3");
    }

    // �����L���O��ʂɑJ�ڂ���R���[�`���Ăяo��
    public void RankingScene()
    {
        StartCoroutine(ChangeRankingScene());
    }

    // �����L���O��ʂɑJ��
    private IEnumerator ChangeRankingScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("RankingScene");
    }

    // �Q�[���N���A��ʂɑJ�ڂ���R���[�`���Ăяo��
    public void GameClearScene()
    {
        StartCoroutine(ChangeGameClearScene());
    }

    // �Q�[���N���A��ʂɑJ��
    private IEnumerator ChangeGameClearScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("GameClearScene");
    }

    // �Q�[���I�[�o�[��ʂɑJ�ڂ���R���[�`���Ăяo��
    public void GameOverScene()
    {
        StartCoroutine(ChangeGameOverScene());
    }

    // �Q�[���I�[�o�[��ʂɑJ��
    private IEnumerator ChangeGameOverScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameOverScene");
    }

    // �^�C�g����ʂɑJ�ڂ���R���[�`���Ăяo��
    public void TitleScene()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(ChangeTitleScene());
    }

    // �^�C�g����ʂɑJ��
    private IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("TitleScene");
    }

    // �Q�[�����I�����鏈��
    public void GameExit()
    {
        Application.Quit();
    }
}

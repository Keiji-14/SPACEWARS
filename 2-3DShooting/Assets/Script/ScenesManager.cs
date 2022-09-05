using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // ステージやスコアの状態、プレイヤーの体力をリセットしてからゲーム画面(ステージ1)に遷移するコルーチン呼び出し
    public void GameScene1()
    {
        PlayerPrefs.SetInt("PLAYERHP", 5);
        PlayerPrefs.DeleteKey("PHASE");
        PlayerPrefs.DeleteKey("SCORE");
        PlayerPrefs.DeleteKey("STAGE");
        StartCoroutine(ChangeGameScene1());
    }

    // ゲーム画面(ステージ1)に遷移
    private IEnumerator ChangeGameScene1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameScene1");
    }

    // ゲーム画面(ステージ2)に遷移
    public void GameScene2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    // ゲーム画面(ステージ3)に遷移
    public void GameScene3()
    {
        SceneManager.LoadScene("GameScene3");
    }

    // ランキング画面に遷移するコルーチン呼び出し
    public void RankingScene()
    {
        StartCoroutine(ChangeRankingScene());
    }

    // ランキング画面に遷移
    private IEnumerator ChangeRankingScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("RankingScene");
    }

    // ゲームクリア画面に遷移するコルーチン呼び出し
    public void GameClearScene()
    {
        StartCoroutine(ChangeGameClearScene());
    }

    // ゲームクリア画面に遷移
    private IEnumerator ChangeGameClearScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("GameClearScene");
    }

    // ゲームオーバー画面に遷移するコルーチン呼び出し
    public void GameOverScene()
    {
        StartCoroutine(ChangeGameOverScene());
    }

    // ゲームオーバー画面に遷移
    private IEnumerator ChangeGameOverScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameOverScene");
    }

    // タイトル画面に遷移するコルーチン呼び出し
    public void TitleScene()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(ChangeTitleScene());
    }

    // タイトル画面に遷移
    private IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("TitleScene");
    }

    // ゲームを終了する処理
    public void GameExit()
    {
        Application.Quit();
    }
}

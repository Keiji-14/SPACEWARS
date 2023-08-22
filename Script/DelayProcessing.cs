using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy時に消滅させた後でも処理を継続させる用
/// （主に消滅時に時間差で処理させるように使用）
/// </summary>
public class DelayProcessing : MonoBehaviour
{
    [Header("GameOver")]
    public bool gameOverCheck = false;

    [Header("LifeBonus")]
    public bool lifeBonusChack = false;
    public bool recoveryCheck = false;
    [SerializeField] GameObject lifeBonusText;

    [Header("Scene")]
    public ScenesManager scenesManager;

    [Header("GetComponent")]
    public Player player;

    void Update()
    {
        StartCoroutine(DelayGameOver());
        StartCoroutine(DelayPlayerLifeRecovery());
    }

    // 自機がやられたときに時間差でゲームオーバー画面に遷移させる
    private IEnumerator DelayGameOver()
    {
        if (gameOverCheck)
        {
            yield return new WaitForSeconds(2.0f);
            scenesManager.GameOverScene();
            gameOverCheck = false;
        }
    }

    //ボスを倒したときにに時間差でプレイヤーの体力を回復する（ライフボーナス）
    private IEnumerator DelayPlayerLifeRecovery()
    {
        if (lifeBonusChack)
        {
            yield return new WaitForSeconds(3.0f);

            // ライフボーナスのテキストを表示
            lifeBonusText.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            // 回復したかどうか
            if (!recoveryCheck)
            {
                recoveryCheck = true;
                player.PlayerLifeRecovery();
            }

            yield return new WaitForSeconds(2.0f);

            lifeBonusText.SetActive(false);

            lifeBonusChack = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy���ɏ��ł�������ł��������p��������p
/// �i��ɏ��Ŏ��Ɏ��ԍ��ŏ���������悤�Ɏg�p�j
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

    // ���@�����ꂽ�Ƃ��Ɏ��ԍ��ŃQ�[���I�[�o�[��ʂɑJ�ڂ�����
    private IEnumerator DelayGameOver()
    {
        if (gameOverCheck)
        {
            yield return new WaitForSeconds(2.0f);
            scenesManager.GameOverScene();
            gameOverCheck = false;
        }
    }

    //�{�X��|�����Ƃ��ɂɎ��ԍ��Ńv���C���[�̗̑͂��񕜂���i���C�t�{�[�i�X�j
    private IEnumerator DelayPlayerLifeRecovery()
    {
        if (lifeBonusChack)
        {
            yield return new WaitForSeconds(3.0f);

            // ���C�t�{�[�i�X�̃e�L�X�g��\��
            lifeBonusText.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            // �񕜂������ǂ���
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

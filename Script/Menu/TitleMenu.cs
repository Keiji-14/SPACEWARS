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

    // �Q�[���̐�����ʂ�\�����鏈��
    public void OpenManual()
    {
        //openManual = true;
        titleSelect.SetActive(false);
        manualMenu.SetActive(true);

        // ������ʂ̃y�[�W���ŏ��ɖ߂�
        PlayerPrefs.SetInt("MANUALPAGE", 1);
    }

    // �Q�[���̐�����ʂ���鏈��
    public void CloseManual()
    {
        titleSelect.SetActive(true);
        manualMenu.SetActive(false);
    }

    // �Q�[���̃I�v�V�������J������
    public void OpenOption()
    {
        titleSelect.SetActive(false);
        optionMenu.SetActive(true); 
    }

    // �Q�[���̃I�v�V��������鏈��
    public void CloseOption()
    {
        titleSelect.SetActive(true);
        optionMenu.SetActive(false);
    }
}

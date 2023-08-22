using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�E�t�F�[�Y�̐�����i�s�Ȃǂ̊Ǘ�
/// </summary>
public class StageManager : MonoBehaviour
{
    public int phaseNum;
    public int stageCount;
    public int phase3_Count;

    [Header("Text")]
    [SerializeField] GameObject stageText;
    [SerializeField] GameObject stageClearText;
    [SerializeField] GameObject phaseText1;
    [SerializeField] GameObject phaseText2;
    [SerializeField] GameObject phaseText3;
    [Header("Phase")]
    [SerializeField] GameObject phase1;
    [SerializeField] GameObject phase2;
    [SerializeField] GameObject phase3;
    [SerializeField] GameObject[] phase3_Object;
    [Header("Boss")]
    [SerializeField] GameObject boss1;
    [SerializeField] GameObject boss2;
    [SerializeField] GameObject boss3;

    [Header("Scene")]
    public ScenesManager scenesManager;

    [Header("Fade")]
    public FadeController fadeController;

    private GameObject player_object;
    Player player;

    private enum Stage
    {
        stage1_1,
        stage1_2,
        stage1_3,
        nextStage2,
        stage2_1,
        stage2_2,
        stage2_3,
        nextStage3,
        stage3_1,
        stage3_2,
        stage3_3,
        clear,
    }

    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        player = player_object.GetComponent<Player>();
    }

    void Update()
    {
        phaseNum = PlayerPrefs.GetInt("PHASE", 0);
        stageCount = PlayerPrefs.GetInt("STAGE", 0);

        NextPhase();
    }

    private void NextPhase()
    {
        switch (stageCount)
        {
            case (int)Stage.stage1_1:
                StartCoroutine(StartPhase1());
                break;
            case (int)Stage.stage1_2:
                Invoke("StartPhase2", 5.0f);
                break;
            case (int)Stage.stage1_3:
                Invoke("StartPhase3", 5.0f);
                break;
            case (int)Stage.nextStage2:
                StartCoroutine(NextStage2());
                break;
            case (int)Stage.stage2_1:
                StartCoroutine(StartPhase1());
                break;
            case (int)Stage.stage2_2:
                Invoke("StartPhase2", 5.0f);
                break;
            case (int)Stage.stage2_3:
                Invoke("StartPhase3", 5.0f);
                break;
            case (int)Stage.nextStage3:
                StartCoroutine(NextStage3());
                break;
            case (int)Stage.stage3_1:
                StartCoroutine(StartPhase1());
                break;
            case (int)Stage.stage3_2:
                Invoke("StartPhase2", 5.0f);
                break;
            case (int)Stage.stage3_3:
                Invoke("StartPhase3", 5.0f);
                break;
            case (int)Stage.clear:
                StartCoroutine(GameClear());
                break;
        }
    }

    // �t�F�[�Y1���J�n���鏈��
    private IEnumerator StartPhase1()
    {
        stageText.SetActive(true);

        // �X�e�[�W�J�n���̃t�F�[�h�C���̂��߂̑҂�����
        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeIn = true;
        phaseText1.SetActive(true);
        
        // �J�������ォ��̈ʒu�ɐݒ肷��
        phaseNum = 0;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // �G��{�X�̔z�u
        Invoke("CreatePhase1", 7.0f);
        Invoke("CreateBoss1", 100.0f);
    }

    // �t�F�[�Y2�Ɉڍs���鏈��
    private void StartPhase2()
    {
        Destroy(phase1.gameObject);
        phaseText2.SetActive(true);

        // �J������������̈ʒu�ɐݒ肷��
        phaseNum = 1;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // �G��{�X�̔z�u
        Invoke("CreatePhase2", 7.0f);
        Invoke("CreateBoss2", 100.0f);
    }

    // �t�F�[�Y3�Ɉڍs���鏈��
    private void StartPhase3()
    {
        Destroy(phase2.gameObject);
        phaseText3.SetActive(true);

        // �J��������O�̈ʒu�ɐݒ�
        phaseNum = 2;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // �G��{�X�̔z�u
        StartCoroutine(CreatePhase3());
        Invoke("CreateBoss3", 100.0f);
    }

    // �X�e�[�W2�Ɉڍs���鏈��
    private IEnumerator NextStage2()
    {
        // ���C�t�{�[�i�X���I������܂ő҂�����
        yield return new WaitForSeconds(7.0f);

        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeOut = true;
        stageCount = 4;
        PlayerPrefs.SetInt("STAGE", stageCount);

        yield return new WaitForSeconds(2.0f);
        
        scenesManager.GameScene2();
    }

    // �X�e�[�W3�Ɉڍs���鏈��
    private IEnumerator NextStage3()
    {
        // ���C�t�{�[�i�X���I������܂ő҂�����
        yield return new WaitForSeconds(7.0f);

        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeOut = true;
        stageCount = 8;
        PlayerPrefs.SetInt("STAGE", stageCount);

        yield return new WaitForSeconds(2.0f);

        scenesManager.GameScene3();
    }

    // �Q�[���N���A��ʂɈڍs���鏈��
    private IEnumerator GameClear()
    {
        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        scenesManager.GameClearScene();
    }

    // �t�F�[�Y1�̏�Q����\������
    private void CreatePhase1()
    {
        phase1.SetActive(true);
    }

    // �t�F�[�Y1�̃{�X��\������
    private void CreateBoss1()
    {
        boss1.SetActive(true);
    }

    // �t�F�[�Y2�̏�Q����\������
    private void CreatePhase2()
    {
        phase2.SetActive(true);
    }

    // �X�e�[�W1�̃t�F�[�Y2�̃{�X��\������
    private void CreateBoss2()
    {
        boss2.SetActive(true);
    }

    // �t�F�[�Y3�̏�Q����\������
    private IEnumerator CreatePhase3()
    {
        yield return new WaitForSeconds(7.0f);
        phase3.SetActive(true);
        // ��莞�Ԃ��Ƃɏo�������Ă���
        for (int i = 0; i < phase3_Count; i++)
        {
            phase3_Object[i].SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }
    }

    // �t�F�[�Y3�̃{�X��\������
    private void CreateBoss3()
    {
        boss3.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ・フェーズの生成や進行などの管理
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

    // フェーズ1を開始する処理
    private IEnumerator StartPhase1()
    {
        stageText.SetActive(true);

        // ステージ開始時のフェードインのための待ち時間
        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeIn = true;
        phaseText1.SetActive(true);
        
        // カメラを上からの位置に設定する
        phaseNum = 0;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // 敵やボスの配置
        Invoke("CreatePhase1", 7.0f);
        Invoke("CreateBoss1", 100.0f);
    }

    // フェーズ2に移行する処理
    private void StartPhase2()
    {
        Destroy(phase1.gameObject);
        phaseText2.SetActive(true);

        // カメラを横からの位置に設定する
        phaseNum = 1;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // 敵やボスの配置
        Invoke("CreatePhase2", 7.0f);
        Invoke("CreateBoss2", 100.0f);
    }

    // フェーズ3に移行する処理
    private void StartPhase3()
    {
        Destroy(phase2.gameObject);
        phaseText3.SetActive(true);

        // カメラを手前の位置に設定
        phaseNum = 2;
        PlayerPrefs.SetInt("PHASE", phaseNum);

        // 敵やボスの配置
        StartCoroutine(CreatePhase3());
        Invoke("CreateBoss3", 100.0f);
    }

    // ステージ2に移行する処理
    private IEnumerator NextStage2()
    {
        // ライフボーナスが終了するまで待たせる
        yield return new WaitForSeconds(7.0f);

        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeOut = true;
        stageCount = 4;
        PlayerPrefs.SetInt("STAGE", stageCount);

        yield return new WaitForSeconds(2.0f);
        
        scenesManager.GameScene2();
    }

    // ステージ3に移行する処理
    private IEnumerator NextStage3()
    {
        // ライフボーナスが終了するまで待たせる
        yield return new WaitForSeconds(7.0f);

        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        fadeController.isFadeOut = true;
        stageCount = 8;
        PlayerPrefs.SetInt("STAGE", stageCount);

        yield return new WaitForSeconds(2.0f);

        scenesManager.GameScene3();
    }

    // ゲームクリア画面に移行する処理
    private IEnumerator GameClear()
    {
        stageClearText.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        scenesManager.GameClearScene();
    }

    // フェーズ1の障害物を表示する
    private void CreatePhase1()
    {
        phase1.SetActive(true);
    }

    // フェーズ1のボスを表示する
    private void CreateBoss1()
    {
        boss1.SetActive(true);
    }

    // フェーズ2の障害物を表示する
    private void CreatePhase2()
    {
        phase2.SetActive(true);
    }

    // ステージ1のフェーズ2のボスを表示する
    private void CreateBoss2()
    {
        boss2.SetActive(true);
    }

    // フェーズ3の障害物を表示する
    private IEnumerator CreatePhase3()
    {
        yield return new WaitForSeconds(7.0f);
        phase3.SetActive(true);
        // 一定時間ごとに出現させていく
        for (int i = 0; i < phase3_Count; i++)
        {
            phase3_Object[i].SetActive(true);
            yield return new WaitForSeconds(4.5f);
        }
    }

    // フェーズ3のボスを表示する
    private void CreateBoss3()
    {
        boss3.SetActive(true);
    }
}

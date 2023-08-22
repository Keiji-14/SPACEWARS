using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BGMを切り替え・継続を行う処理
/// </summary>
public class BGM : MonoBehaviour
{
    private static BGM instance;

    private string nowScene;

    public AudioSource[] bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        nowScene = "TitleScene";

        // 起動時のBGMを流す
        bgm[0].Play();

        //シーンが切り替わった時に呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わった時に呼ばれるメソッド　
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // BGM1を流す処理(タイトルなど画面)
        if (nextScene.name == "TitleScene")
        {
            bgm[0].Play();
            bgm[1].Stop();
            bgm[2].Stop();
            bgm[3].Stop();
        }
        // BGM2を流す処理(ゲーム画面)
        if (nextScene.name == "GameScene1")
        {
            bgm[0].Stop();
            bgm[1].Play();
            bgm[2].Stop();
            bgm[3].Stop();
        }
        // BGM3を流す処理(ゲームオーバー画面)
        if (nextScene.name == "GameOverScene")
        {
            bgm[0].Stop();
            bgm[1].Stop();
            bgm[2].Play();
            bgm[3].Stop();
        }
        // BGM4を流す処理(ゲームクリア画面、ランキング画面)
        if (nowScene == "TitleScene" && nextScene.name == "RankingScene" ||
            nextScene.name == "GameClearScene")
        {
            bgm[0].Stop();
            bgm[1].Stop();
            bgm[2].Stop();
            bgm[3].Play();
        }

        //遷移後のシーン名を「１つ前のシーン名」として保持
        nowScene = nextScene.name;
    }
}

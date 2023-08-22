using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BGM��؂�ւ��E�p�����s������
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

        // �N������BGM�𗬂�
        bgm[0].Play();

        //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h��o�^
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //�V�[�����؂�ւ�������ɌĂ΂�郁�\�b�h�@
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        // BGM1�𗬂�����(�^�C�g���Ȃǉ��)
        if (nextScene.name == "TitleScene")
        {
            bgm[0].Play();
            bgm[1].Stop();
            bgm[2].Stop();
            bgm[3].Stop();
        }
        // BGM2�𗬂�����(�Q�[�����)
        if (nextScene.name == "GameScene1")
        {
            bgm[0].Stop();
            bgm[1].Play();
            bgm[2].Stop();
            bgm[3].Stop();
        }
        // BGM3�𗬂�����(�Q�[���I�[�o�[���)
        if (nextScene.name == "GameOverScene")
        {
            bgm[0].Stop();
            bgm[1].Stop();
            bgm[2].Play();
            bgm[3].Stop();
        }
        // BGM4�𗬂�����(�Q�[���N���A��ʁA�����L���O���)
        if (nowScene == "TitleScene" && nextScene.name == "RankingScene" ||
            nextScene.name == "GameClearScene")
        {
            bgm[0].Stop();
            bgm[1].Stop();
            bgm[2].Stop();
            bgm[3].Play();
        }

        //�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
        nowScene = nextScene.name;
    }
}

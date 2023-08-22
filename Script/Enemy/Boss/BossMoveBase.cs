using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveBase : MonoBehaviour
{
    public int patternNum;
    public float bossHp;
    public float bossSpeed;
    public float appearTime;
    public float appearBossSpeed;
    public float moveAmplitudeX;
    public float moveAmplitudeY;
    public float moveAmplitudeZ;
    public float bossBulletSpeed;
    public float bossBulletInterval;

    [HideInInspector] public int score;
    [HideInInspector] public int stageCount;
    [HideInInspector] public float bulletTimeElapsed;
    [HideInInspector] public bool shotType = true;
    [HideInInspector] public bool bossCheck = false;
    [HideInInspector] public bool appearBossCheck = false;
    [HideInInspector] public bool rendered = false;
    [HideInInspector] public bool hitCollider = false;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    [Header("EnemyBullet")]
    public GameObject bossBullet;

    [Header("SetPosition")]
    public float bossSetPosX;
    public float bossSetPosY;
    public float bossSetPosZ;

    [Header("Expload")]
    [SerializeField] int exploadNum;
    [SerializeField] float exploadScaleX;
    [SerializeField] float exploadScaleY;
    [SerializeField] float exploadScaleZ;
    [SerializeField] ParticleSystem[] exploadEffect;

    [Header("SE")]
    [SerializeField] AudioClip destroySE;

    [Header("GetComponent")]
    public DelayProcessing delayProcessing;

    public enum Pattern
    {
        pattern1,
        pattern2,
        pattern3,
    }

    private enum Boss
    {
        boss1_1,
        boss1_2,
        boss1_3,
        boss2_1,
        boss2_2,
        boss2_3,
        boss3_1,
        boss3_2,
        boss3_3,
    }

    void Update()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        stageCount = PlayerPrefs.GetInt("STAGE", 0);
    }

    // カメラに移ったかの確認処理
    public void OnWillRenderObject()
    {
        //メインカメラに映った時だけ Rendered を有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            rendered = true;
        }
    }

    // 倒した時の消滅演出の処理
    public void Expload()
    {
        for (int i = 0; i < exploadNum; i++)
        {
            exploadEffect[i].transform.localScale = new Vector3(exploadScaleX * 0.01f, exploadScaleY * 0.01f, exploadScaleZ * 0.01f);
            Instantiate(exploadEffect[i], transform.position, Quaternion.identity);
        }
        AudioSource.PlayClipAtPoint(destroySE, transform.position);
    }
}

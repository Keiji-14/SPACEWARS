using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveBase : MonoBehaviour
{
    public int enemyPatternNum;
    public int patternNum;
    public int enemyHp;
    public float enemySpeed;
    public float amplitude;
    public float enemyBulletSpeed;
    public float enemyBulletInterval;
    public float shotStartTime;
    public float timeElapsed;
    public float destroyTime;

    [HideInInspector] public int score;
    [HideInInspector] public float bulletTimeElapsed;

    public bool enemyCheck = false;
    public bool rendered = false;
    public bool hitCollider = false;

    public const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    public GameObject target;
    public GameObject enemyBullet;

    [Header("Item")]
    public GameObject recoveryItem;

    [Header("SetPosition")]
    public float enemySetPosX;
    public float enemySetPosY;
    public float enemySetPosZ;

    [Header("Expload")]
    [SerializeField] int exploadNum;
    [SerializeField] float exploadScaleX;
    [SerializeField] float exploadScaleY;
    [SerializeField] float exploadScaleZ;
    [SerializeField] ParticleSystem[] exploadEffect;

    [Header("SE")]
    [SerializeField] AudioClip destroySE;

    void Update()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
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

    // やられた時の消滅演出の処理
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

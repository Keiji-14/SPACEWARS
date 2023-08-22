using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeteorMove : MonoBehaviour
{
    [SerializeField] float meteorHp;
    [SerializeField] float meteorSpeed;
    [SerializeField] float meteorType;
    [SerializeField] float meteorRotateX;
    [SerializeField] float meteorRotateY;
    [SerializeField] float meteorRotateZ;

    private int score;
    private bool rendered = false;
    private bool hitCollider = false;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    [Header("Item")]
    [SerializeField] GameObject recoveryItem;

    [Header("Expload")]
    [SerializeField] float exploadScaleX;
    [SerializeField] float exploadScaleY;
    [SerializeField] float exploadScaleZ;
    [SerializeField] ParticleSystem exploadEffect;

    [Header("SE")]
    [SerializeField] AudioClip destroySE;

    private enum Meteor
    {
        meteor1,
        meteor2,
        meteor3,
        meteor4,
        meteor5,
    }

    void Update()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(meteorRotateX * 0.1f, meteorRotateY * 0.1f, meteorRotateZ * 0.1f));

        if (rendered)
        {
            hitCollider = true;
            transform.position += Vector3.forward * meteorSpeed * -0.01f;
        }
        else
        {
            transform.position += Vector3.forward * -0.02f;
        }

        rendered = false;
    }

    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ Rendered を有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            rendered = true;
        }
    }

    // やられた時の消滅する処理
    private void Expload()
    {
        exploadEffect.transform.localScale = new Vector3(exploadScaleX * 0.01f, exploadScaleY * 0.01f, exploadScaleZ * 0.01f);
        Instantiate(exploadEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(destroySE, transform.position);
        Destroy(this.gameObject);
    }

    private void CrushMeteor()
    {
        // 確率でアイテムが出現
        int itemProbability = Random.Range(0, 20);
        switch (itemProbability)
        {
            case 0:
                Instantiate(recoveryItem, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        // 隕石の種類によって獲得するスコアが変動
        switch (meteorType)
        {
            case (int)Meteor.meteor1:
                score += 100;
                break;
            case (int)Meteor.meteor2:
                score += 200;
                break;
            case (int)Meteor.meteor3:
                score += 300;
                break;
            case (int)Meteor.meteor4:
                score += 400;
                break;
            case (int)Meteor.meteor5:
                score += 500;
                break;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 当たり判定が有効になっているかどうか
        if (hitCollider)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                meteorHp -= 1;
                if (meteorHp <= 0)
                {
                    Expload();
                    CrushMeteor();
                }
                PlayerPrefs.SetInt("SCORE", score);
            }
            // 隕石を消す壁に衝突した時に隕石を削除する
            if (collision.gameObject.CompareTag("Object Wall"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

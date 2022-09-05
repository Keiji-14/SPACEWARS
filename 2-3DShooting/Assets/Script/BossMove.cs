using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public int stageBossNum;
    public int stageCount;

    public float bossHp;
    public float bossSpeed;
    public float appearTime;
    public float appearBossSpeed;
    public float moveAmplitudeX;
    public float moveAmplitudeY;
    public float moveAmplitudeZ;
    public float bossBulletSpeed;
    public float bossBulletInterval;

    private int score;
    private float bulletTimeElapsed;
    private bool shotType = true;
    private bool bossCheck = false;
    private bool appearBossCheck = false;
    private bool rendered = false;
    private bool hitCollider = false;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    [SerializeField] GameObject bossBullet;

    [Header("SetPosition")]
    [SerializeField] float bossSetPosX;
    [SerializeField] float bossSetPosY;
    [SerializeField] float bossSetPosZ;

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

    void FixedUpdate()
    {
        bulletTimeElapsed += Time.deltaTime;

        if (rendered)
        {
            BossMovePattern();
        }
        else
        {
            transform.position += Vector3.forward * -0.02f;
        }

        rendered = false;
    }

    // カメラに移ったかの確認処理
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ Rendered を有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            rendered = true;
        }
    }

    // 倒した時の消滅する処理
    private void Expload()
    {
        for (int i = 0; i < exploadNum; i++)
        {
            exploadEffect[i].transform.localScale = new Vector3(exploadScaleX * 0.01f, exploadScaleY * 0.01f, exploadScaleZ * 0.01f);
            Instantiate(exploadEffect[i], transform.position, Quaternion.identity);
        }
        AudioSource.PlayClipAtPoint(destroySE, transform.position);
    }

    // ボスを撃破した時に次のフェーズやステージに移行する為のカウントの設定
    // ボスによって獲得するスコアが変動
    private void CrushBoss()
    {
        switch (stageBossNum)
        {
            case (int)Boss.boss1_1:
                score += 10000;
                stageCount = 1;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss1_2:
                score += 10000;
                stageCount = 2;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss1_3:
                score += 10000;
                stageCount = 3;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss2_1:
                score += 15000;
                stageCount = 5;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss2_2:
                score += 15000;
                stageCount = 6;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss2_3:
                score += 15000;
                stageCount = 7;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss3_1:
                score += 20000;
                stageCount = 9;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss3_2:
                score += 20000;
                stageCount = 10;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Boss.boss3_3:
                score += 20000;
                stageCount = 11;
                break;
        }
        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.SetInt("STAGE", stageCount);
    }

    // 敵の当たり判定の設定と処理
    public void OnCollisionEnter(Collision collision)
    {
        if (hitCollider)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                bossHp -= 1;
                if (bossHp <= 0)
                {
                    Expload();
                    CrushBoss();

                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void BossMovePattern()
    {
        switch (stageBossNum)
        {
            case (int)Boss.boss1_1:
                StartCoroutine(Boss1());
                break;
            case (int)Boss.boss1_2:
                StartCoroutine(Boss2());
                break;
            case (int)Boss.boss1_3:
                StartCoroutine(Boss3());
                break;
            case (int)Boss.boss2_1:
                StartCoroutine(Boss4());
                break;
            case (int)Boss.boss2_2:
                StartCoroutine(Boss5());
                break;
            case (int)Boss.boss2_3:
                StartCoroutine(Boss6());
                break;
            case (int)Boss.boss3_1:
                StartCoroutine(Boss7());
                break;
            case (int)Boss.boss3_2:
                StartCoroutine(Boss8());
                break;
            case (int)Boss.boss3_3:
                StartCoroutine(Boss9());
                break;
        }
    }

    // ステージ1-1のボスの処理
    private IEnumerator Boss1()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        // 出現が完了したら当たり判定を有効にする
        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.0f);
                Vector3 leftBulletPos = new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z - 0.5f);
                Vector3 rightBulletPos = new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z - 0.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var leftShotBullet = Instantiate(bossBullet, leftBulletPos, Quaternion.identity);
                var rightShotBullet = Instantiate(bossBullet, rightBulletPos, Quaternion.identity);
                centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                leftShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                rightShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ1-2のボスの処理
    private IEnumerator Boss2()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.y < bossSetPosY - moveAmplitudeY)
            {
                bossCheck = false;
            }
            if (transform.position.y > bossSetPosY + moveAmplitudeY)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.up * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.up * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 3.0f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var upDiffusionShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                upDiffusionShotBullet.GetComponent<Rigidbody>().velocity = Vector3.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet.GetComponent<Rigidbody>().velocity = -Vector3.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ1-3のボスの処理
    private IEnumerator Boss3()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);

                if (shotType)
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;

                   shotType = false;
                }
                else
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    
                    shotType = true;
                }
                bulletTimeElapsed = 0.0f;

            }
        }
    }

    // ステージ2-1のボスの処理
    private IEnumerator Boss4()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.0f);
                Vector3 leftBulletPos = new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z - 0.5f);
                Vector3 rightBulletPos = new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z - 0.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var leftShotBullet1 = Instantiate(bossBullet, leftBulletPos, Quaternion.identity);
                var leftShotBullet2 = Instantiate(bossBullet, leftBulletPos, Quaternion.identity);
                var rightShotBullet1 = Instantiate(bossBullet, rightBulletPos, Quaternion.identity);
                var rightShotBullet2 = Instantiate(bossBullet, rightBulletPos, Quaternion.identity);
                centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                leftShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                leftShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                rightShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                rightShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ2-2のボスの処理
    private IEnumerator Boss5()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.y < bossSetPosY - moveAmplitudeY)
            {
                bossCheck = false;
            }
            if (transform.position.y > bossSetPosY + moveAmplitudeY)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.up * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.up * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                var upDiffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var upDiffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                upDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                upDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = transform.right.normalized * 3.0f + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 3.0f + transform.forward.normalized * -bossBulletSpeed;
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ2-3のボスの処理
    private IEnumerator Boss6()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet3 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet4 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);

                if (shotType)
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet3.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet4.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;

                    shotType = false;
                }
                else
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet3.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet4.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;

                    shotType = true;
                }
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ3-1のボスの処理
    private IEnumerator Boss7()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.0f);
                Vector3 leftBulletPos = new Vector3(transform.position.x - 4.0f, transform.position.y, transform.position.z - 0.5f);
                Vector3 rightBulletPos = new Vector3(transform.position.x + 4.0f, transform.position.y, transform.position.z - 0.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var centerDiffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var centerDiffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var leftDiffusionShotBullet1 = Instantiate(bossBullet, leftBulletPos, Quaternion.identity);
                var leftDiffusionShotBullet2 = Instantiate(bossBullet, leftBulletPos, Quaternion.identity);
                var rightDiffusionShotBullet1 = Instantiate(bossBullet, rightBulletPos, Quaternion.identity);
                var rightDiffusionShotBullet2 = Instantiate(bossBullet, rightBulletPos, Quaternion.identity);
                centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                centerDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                centerDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                leftDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                leftDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                rightDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                rightDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ3-2のボスの処理
    private IEnumerator Boss8()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.y < bossSetPosY - moveAmplitudeY)
            {
                bossCheck = false;
            }
            if (transform.position.y > bossSetPosY + moveAmplitudeY)
            {
                bossCheck = true;
            }
            if (bossCheck)
            {
                transform.position -= Vector3.up * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.up * bossSpeed;
            }
            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                var upDiffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var upDiffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var upDiffusionShotBullet3 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var downDiffusionShotBullet3 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                upDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                upDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = transform.right.normalized * 3.0f + transform.forward.normalized * -bossBulletSpeed;
                upDiffusionShotBullet3.GetComponent<Rigidbody>().velocity = transform.right.normalized * 5.0f + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet1.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 3.0f + transform.forward.normalized * -bossBulletSpeed;
                downDiffusionShotBullet3.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 5.0f + transform.forward.normalized * -bossBulletSpeed;

                bulletTimeElapsed = 0.0f;
            }
        }
    }

    // ステージ3-3のボスの処理
    private IEnumerator Boss9()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        if (appearBossCheck)
        {
            hitCollider = true;
            this.tag = ("Enemy");
            if (transform.position.x < bossSetPosX - moveAmplitudeX)
            {
                bossCheck = false;
            }
            if (transform.position.x > bossSetPosX + moveAmplitudeX)
            {
                bossCheck = true;
            }

            if (bossCheck)
            {
                transform.position -= Vector3.right * bossSpeed;
            }
            if (!bossCheck)
            {
                transform.position += Vector3.right * bossSpeed;
            }

            if (bulletTimeElapsed >= bossBulletInterval)
            {
                Vector3 centerBulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);

                var centerShotBullet = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet1 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet2 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet3 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet4 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet5 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);
                var diffusionShotBullet6 = Instantiate(bossBullet, centerBulletPos, Quaternion.identity);

                if (shotType)
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + -transform.right.normalized * 1.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + transform.right.normalized * 1.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet3.GetComponent<Rigidbody>().velocity = transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet4.GetComponent<Rigidbody>().velocity = -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet5.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + -transform.right.normalized * 1.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet6.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + transform.right.normalized * 1.0f + transform.forward.normalized * -bossBulletSpeed;

                    shotType = false;
                }
                else
                {
                    centerShotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet2.GetComponent<Rigidbody>().velocity = transform.up.normalized * 1.0f + -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet3.GetComponent<Rigidbody>().velocity = transform.up.normalized * 1.0f + transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet4.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 1.0f + -transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet5.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 1.0f + transform.right.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;
                    diffusionShotBullet6.GetComponent<Rigidbody>().velocity = -transform.up.normalized * 2.0f + transform.forward.normalized * -bossBulletSpeed;

                    shotType = true;
                }
                bulletTimeElapsed = 0.0f;

            }
        }
    }
}

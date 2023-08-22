using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フェーズ3のボスの処理
/// </summary>
public class BossType3 : BossMoveBase
{
    void FixedUpdate()
    {
        bulletTimeElapsed += Time.deltaTime;

        if (rendered)
        {
            MovePattern();
        }
        else
        {
            transform.position += Vector3.forward * -0.02f;
        }

        rendered = false;
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
                    Crush();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // ステージ毎に行動パターンを変更する
    private void MovePattern()
    {
        switch (patternNum)
        {
            case (int)Pattern.pattern1:
                StartCoroutine(BossMovePattern1());
                break;
            case (int)Pattern.pattern2:
                StartCoroutine(BossMovePattern2());
                break;
            case (int)Pattern.pattern3:
                StartCoroutine(BossMovePattern3());
                break;
        }
    }

    // ボスを撃破した時に次のフェーズやステージに移行する為のカウントの設定
    // ステージのボスによって獲得するスコアが変動
    private void Crush()
    {
        switch (patternNum)
        {
            case (int)Pattern.pattern1:
                score += 10000;
                stageCount = 3;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
            case (int)Pattern.pattern2:
                score += 15000;
                stageCount = 7;
                delayProcessing.lifeBonusChack = true;
                delayProcessing.recoveryCheck = false;
                break;
                // 最後ボスはライフボーナスは行わない
            case (int)Pattern.pattern3:
                score += 20000;
                stageCount = 11;
                break;
        }

        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.SetInt("STAGE", stageCount);
    }

    // ステージ1のボスの動き
    private IEnumerator BossMovePattern1()
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

    // ステージ2のボスの動き
    private IEnumerator BossMovePattern2()
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

    // ステージ3のボスの動き
    private IEnumerator BossMovePattern3()
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

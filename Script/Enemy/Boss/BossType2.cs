using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フェーズ2のボスの処理
/// </summary>
public class BossType2 : BossMoveBase
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
                stageCount = 2;
                break;
            case (int)Pattern.pattern2:
                score += 15000;
                stageCount = 6;
                break;
            case (int)Pattern.pattern3:
                score += 20000;
                stageCount = 10;
                break;
        }
        delayProcessing.lifeBonusChack = true;
        delayProcessing.recoveryCheck = false;

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
}

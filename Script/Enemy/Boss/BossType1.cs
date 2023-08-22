using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �t�F�[�Y1�̃{�X�̏���
/// </summary>
public class BossType1 : BossMoveBase
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

    // �G�̓����蔻��̐ݒ�Ə���
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

    // �X�e�[�W���ɍs���p�^�[����ύX����
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

    // �{�X�����j�������Ɏ��̃t�F�[�Y��X�e�[�W�Ɉڍs����ׂ̃J�E���g�̐ݒ�
    // �X�e�[�W�̃{�X�ɂ���Ċl������X�R�A���ϓ�
    private void Crush()
    {
        switch (patternNum)
        {
            case (int)Pattern.pattern1:
                score += 10000;
                stageCount = 1;
                break;
            case (int)Pattern.pattern2:
                score += 15000;
                stageCount = 5;
                break;
            case (int)Pattern.pattern3:
                score += 20000;
                stageCount = 9;
                break;
        }
        delayProcessing.lifeBonusChack = true;
        delayProcessing.recoveryCheck = false;

        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.SetInt("STAGE", stageCount);
    }

    // �X�e�[�W1�̃{�X�̓���
    private IEnumerator BossMovePattern1()
    {
        if (!appearBossCheck)
        {
            Vector3 bossSetPos = new Vector3(bossSetPosX, bossSetPosY, bossSetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, bossSetPos, appearBossSpeed);
            yield return new WaitForSeconds(appearTime);

            appearBossCheck = true;
        }

        // �o�������������瓖���蔻���L���ɂ���
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

    // �X�e�[�W2�̃{�X�̓���
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

    // �X�e�[�W3�̃{�X�̓���
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
}

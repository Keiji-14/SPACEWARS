using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W1����X�e�[�W3�܂Œʂ��ďo������G�̏���
/// </summary>
public class EnemyType1 : EnemyMoveBase
{
    private enum Pattern
    {
        pattern1,
        pattern2,
        pattern3,
        pattern4,
        pattern5,
        pattern6,
        pattern7,
        pattern8,
        pattern9,
        pattern10,
        pattern11,
    }

    void FixedUpdate()
    {
        bulletTimeElapsed += Time.deltaTime;
        // �G���v���C���[�̕����Ɍ�������
        transform.LookAt(target.transform);

        if (rendered)
        {
            hitCollider = true;
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
            // �v���C���[�̒e�ɓ�������HP��0�ɂȂ��������
            if (collision.gameObject.CompareTag("Bullet"))
            {
                enemyHp -= 1;
                if (enemyHp <= 0)
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
                StartCoroutine(EnemyMovePattern1());
                break;
            case (int)Pattern.pattern2:
                StartCoroutine(EnemyMovePattern2());
                break;
            case (int)Pattern.pattern3:
                StartCoroutine(EnemyMovePattern3());
                break;
            case (int)Pattern.pattern4:
                StartCoroutine(EnemyMovePattern4());
                break;
            case (int)Pattern.pattern5:
                StartCoroutine(EnemyMovePattern5());
                break;
            case (int)Pattern.pattern6:
                StartCoroutine(EnemyMovePattern6());
                break;
            case (int)Pattern.pattern7:
                StartCoroutine(EnemyMovePattern7());
                break;
            case (int)Pattern.pattern8:
                StartCoroutine(EnemyMovePattern8());
                break;
            case (int)Pattern.pattern9:
                StartCoroutine(EnemyMovePattern9());
                break;
            case (int)Pattern.pattern10:
                StartCoroutine(EnemyMovePattern10());
                break;
            case (int)Pattern.pattern11:
                StartCoroutine(EnemyMovePattern11());
                break;
        }
    }

    public void Crush()
    {
        // �m���ŃA�C�e�����o��
        int itemProbability = Random.Range(0, 20);
        switch (itemProbability)
        {
            case 0:
                Instantiate(recoveryItem, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        score += 200;
        PlayerPrefs.SetInt("SCORE", score);
    }

    // �w�肵���ʒu�Ɉړ������̂��Ɏ��Ԃ��o�߂�����-Z���W�Ɍ������Ĉړ�����ł�����
    private IEnumerator EnemyMovePattern1()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // �w�肳�ꂽ���W�ɒ��������Ɏˌ��̏���
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // �ˌ��J�n�܂ł̎���
                yield return new WaitForSeconds(shotStartTime);

                // �G�̎ˌ��̔��ˊԊu�̏���
                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // �ˌ��J�n���Ă��玞�Ԍo�ߌ�Ɏˌ����~�߂�
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.forward * enemySpeed;
            // ���Ԍo�߂œG���폜����
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // �w�肵���ʒu�Ɉړ������̂��Ɏ��Ԃ��o�߂�����-X���W�Ɍ������Ĉړ�����ł�����
    private IEnumerator EnemyMovePattern2()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // �w�肳�ꂽ���W�ɒ��������Ɏˌ��̏���
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // �ˌ��J�n�܂ł̎���
                yield return new WaitForSeconds(shotStartTime);

                // �G�̎ˌ��̔��ˊԊu�̏���
                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // �ˌ��J�n���Ă��玞�Ԍo�ߌ�Ɏˌ����~�߂�
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.right * enemySpeed;
            // ���Ԍo�߂œG���폜����
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // �w�肵���ʒu�Ɉړ������̂��Ɏ��Ԃ��o�߂�����X���W�Ɍ������Ĉړ�����ł�����
    private IEnumerator EnemyMovePattern3()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // �w�肳�ꂽ���W�ɒ��������Ɏˌ��̏���
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // �ˌ��J�n�܂ł̎���
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // �ˌ��J�n���Ă��玞�Ԍo�ߌ�Ɏˌ����~�߂�
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position += Vector3.right * enemySpeed;
            // ���Ԍo�߂œG���폜����
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // �w�肵���ʒu�Ɉړ������̂��Ɏ��Ԃ��o�߂�����-Y���W�Ɍ������Ĉړ�����ł�����
    private IEnumerator EnemyMovePattern4()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // �w�肳�ꂽ���W�ɒ��������Ɏˌ��̏���
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // �ˌ��J�n�܂ł̎���
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // �ˌ��J�n���Ă��玞�Ԍo�ߌ�Ɏˌ����~�߂�
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.up * enemySpeed;
            // ���Ԍo�߂œG���폜����
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }
    // �w�肵���ʒu�Ɉړ������̂��Ɏ��Ԃ��o�߂�����Y���W�Ɍ������Ĉړ�����ł�����
    private IEnumerator EnemyMovePattern5()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // �w�肳�ꂽ���W�ɒ��������Ɏˌ��̏���
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // �ˌ��J�n�܂ł̎���
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // �ˌ��J�n���Ă��玞�Ԍo�ߌ�Ɏˌ����~�߂�
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position += Vector3.up * enemySpeed;
            // ���Ԍo�߂œG���폜����
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }


    // ���ɃW�O�U�O�┽�ˌn�Ȃǂ̓����ȂǂɎg�p
    private IEnumerator EnemyMovePattern6()
    {
        if (transform.position.x < enemySetPosX - amplitude)
        {
            enemyCheck = false;
        }
        if (transform.position.x > enemySetPosX + amplitude)
        {
            enemyCheck = true;
        }

        if (enemyCheck)
        {
            transform.position -= Vector3.right * enemySpeed;
        }
        if (!enemyCheck)
        {
            transform.position += Vector3.right * enemySpeed;
        }

        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);

        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // �c�ɃW�O�U�O�┽�ˌn�Ȃǂ̓����ȂǂɎg�p
    private IEnumerator EnemyMovePattern7()
    {
        if (transform.position.y < enemySetPosY - amplitude)
        {
            enemyCheck = false;
        }
        if (transform.position.y > enemySetPosY + amplitude)
        {
            enemyCheck = true;
        }

        if (enemyCheck)
        {
            transform.position -= Vector3.up * enemySpeed;
        }
        if (!enemyCheck)
        {
            transform.position += Vector3.up * enemySpeed;
        }

        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);

        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // �E�Ɍ��������炩�ȋȐ��ړ�
    private IEnumerator EnemyMovePattern8()
    {
        timeElapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Mathf.Sin(timeElapsed) * amplitude, transform.position.y, transform.position.z);
        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);

        // �G�̒e�̔��˂Ɣ��ˊԊu�̏���
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // ���Ɍ��������炩�ȋȐ��ړ�
    private IEnumerator EnemyMovePattern9()
    {
        timeElapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x - Mathf.Sin(timeElapsed) * amplitude, transform.position.y, transform.position.z);
        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // ��Ɍ��������炩�ȋȐ��ړ�
    private IEnumerator EnemyMovePattern10()
    {
        timeElapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(timeElapsed) * amplitude, transform.position.z);
        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // ���Ɍ��������炩�ȋȐ��ړ�
    private IEnumerator EnemyMovePattern11()
    {
        timeElapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Sin(timeElapsed) * amplitude, transform.position.z);
        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}

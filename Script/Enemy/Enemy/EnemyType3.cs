using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W3�ɏo�������^�̓G�̏���
/// </summary>
public class EnemyType3 : EnemyMoveBase
{
    private enum Pattern
    {
        pattern1,
        pattern2,
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

        score += 2000;
        PlayerPrefs.SetInt("SCORE", score);
    }

    

    // ��^�̓G(�t�F�[�Y1)�̏���
    private IEnumerator EnemyMovePattern1()
    {
        transform.position -= Vector3.forward * enemySpeed;

        // �ˌ��J�n�܂ł̎���
        yield return new WaitForSeconds(shotStartTime);

        // �G�̎ˌ��̔��ˊԊu�̏���
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            var shotBullet1 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            var shotBullet2 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            shotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * enemyBulletSpeed;
            shotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // ��^�̓G(�t�F�[�Y2)�̏���
    private IEnumerator EnemyMovePattern2()
    {
        transform.position -= Vector3.forward * enemySpeed;

        // �ˌ��J�n�܂ł̎���
        yield return new WaitForSeconds(shotStartTime);

        // �G�̎ˌ��̔��ˊԊu�̏���
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            var shotBullet1 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            var shotBullet2 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            shotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized + transform.forward.normalized * enemyBulletSpeed;
            shotBullet2.GetComponent<Rigidbody>().velocity = -transform.up.normalized + transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // ���Ԍo�߂œG���폜����
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}

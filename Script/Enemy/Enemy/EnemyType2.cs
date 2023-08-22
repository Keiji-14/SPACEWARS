using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ɂԂ������玩��������U�^�̓G�̏���
/// </summary>
public class EnemyType2 : EnemyMoveBase
{
    void FixedUpdate()
    {
        // �G���v���C���[�̕����Ɍ�������
        transform.LookAt(target.transform);

        if (rendered)
        {
            hitCollider = true;
            StartCoroutine(EnemyMove());
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
            // �v���C���[�ɂԂ������玩������
            if (collision.gameObject.CompareTag("Player"))
            {
                Expload();
                Destroy(this.gameObject);
            }
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

        score += 100;
        PlayerPrefs.SetInt("SCORE", score);
    }

    private IEnumerator EnemyMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed);

        // �v���C���[���_���[�W�i���G�j���ɏd�Ȃ����ꍇ�Ɏ�������悤�ɐݒ�
        // ���G�I�����Ƀ_���[�W���󂯂Ȃ��悤�ɑ΍�
        if (transform.position == target.transform.position)
        {
            Expload();
            Destroy(this.gameObject);
        }
        // ���Ԍo�߂œG������������
        yield return new WaitForSeconds(destroyTime);
        Expload();
        Destroy(this.gameObject);
    }
}

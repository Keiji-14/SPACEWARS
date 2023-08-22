using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        Invoke("DelayDestroy", 10.0f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɏՓ˂������ɒe���폜����
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        // �e�������ǂɏՓ˂������ɒe���폜����
        if (collision.gameObject.CompareTag("Object Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void DelayDestroy()
    {
        Destroy(this.gameObject);
    }
}

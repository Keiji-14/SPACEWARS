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
        // プレイヤーに衝突した時に弾を削除する
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        // 弾を消す壁に衝突した時に弾を削除する
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

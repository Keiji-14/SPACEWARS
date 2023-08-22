using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーにぶつかったら自爆する特攻型の敵の処理
/// </summary>
public class EnemyType2 : EnemyMoveBase
{
    void FixedUpdate()
    {
        // 敵がプレイヤーの方向に向く処理
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

    // 敵の当たり判定の設定と処理
    public void OnCollisionEnter(Collision collision)
    {
        if (hitCollider)
        {
            // プレイヤーの弾に当たってHPが0になったら消滅
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
            // プレイヤーにぶつかったら自爆する
            if (collision.gameObject.CompareTag("Player"))
            {
                Expload();
                Destroy(this.gameObject);
            }
        }
    }

    public void Crush()
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

        score += 100;
        PlayerPrefs.SetInt("SCORE", score);
    }

    private IEnumerator EnemyMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed);

        // プレイヤーがダメージ（無敵）時に重なった場合に自爆するように設定
        // 無敵終了時にダメージを受けないように対策
        if (transform.position == target.transform.position)
        {
            Expload();
            Destroy(this.gameObject);
        }
        // 時間経過で敵を自爆させる
        yield return new WaitForSeconds(destroyTime);
        Expload();
        Destroy(this.gameObject);
    }
}

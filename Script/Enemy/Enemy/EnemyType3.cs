using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ3に出現する大型の敵の処理
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
        // 敵がプレイヤーの方向に向く処理
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
        }
    }

    // ステージ毎に行動パターンを変更する
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

        score += 2000;
        PlayerPrefs.SetInt("SCORE", score);
    }

    

    // 大型の敵(フェーズ1)の処理
    private IEnumerator EnemyMovePattern1()
    {
        transform.position -= Vector3.forward * enemySpeed;

        // 射撃開始までの時間
        yield return new WaitForSeconds(shotStartTime);

        // 敵の射撃の発射間隔の処理
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            var shotBullet1 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            var shotBullet2 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            shotBullet1.GetComponent<Rigidbody>().velocity = transform.right.normalized + transform.forward.normalized * enemyBulletSpeed;
            shotBullet2.GetComponent<Rigidbody>().velocity = -transform.right.normalized + transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 大型の敵(フェーズ2)の処理
    private IEnumerator EnemyMovePattern2()
    {
        transform.position -= Vector3.forward * enemySpeed;

        // 射撃開始までの時間
        yield return new WaitForSeconds(shotStartTime);

        // 敵の射撃の発射間隔の処理
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            var shotBullet1 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            var shotBullet2 = Instantiate(enemyBullet, bulletPos, Quaternion.identity);
            shotBullet1.GetComponent<Rigidbody>().velocity = transform.up.normalized + transform.forward.normalized * enemyBulletSpeed;
            shotBullet2.GetComponent<Rigidbody>().velocity = -transform.up.normalized + transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}

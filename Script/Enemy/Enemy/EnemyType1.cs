using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ1からステージ3まで通して出現する敵の処理
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

        score += 200;
        PlayerPrefs.SetInt("SCORE", score);
    }

    // 指定した位置に移動したのちに時間が経過したら-Z座標に向かって移動後消滅させる
    private IEnumerator EnemyMovePattern1()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // 指定された座標に着いた時に射撃の準備
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // 射撃開始までの時間
                yield return new WaitForSeconds(shotStartTime);

                // 敵の射撃の発射間隔の処理
                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // 射撃開始してから時間経過後に射撃を止める
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.forward * enemySpeed;
            // 時間経過で敵を削除する
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // 指定した位置に移動したのちに時間が経過したら-X座標に向かって移動後消滅させる
    private IEnumerator EnemyMovePattern2()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // 指定された座標に着いた時に射撃の準備
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // 射撃開始までの時間
                yield return new WaitForSeconds(shotStartTime);

                // 敵の射撃の発射間隔の処理
                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // 射撃開始してから時間経過後に射撃を止める
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.right * enemySpeed;
            // 時間経過で敵を削除する
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // 指定した位置に移動したのちに時間が経過したらX座標に向かって移動後消滅させる
    private IEnumerator EnemyMovePattern3()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // 指定された座標に着いた時に射撃の準備
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // 射撃開始までの時間
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // 射撃開始してから時間経過後に射撃を止める
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position += Vector3.right * enemySpeed;
            // 時間経過で敵を削除する
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }

    // 指定した位置に移動したのちに時間が経過したら-Y座標に向かって移動後消滅させる
    private IEnumerator EnemyMovePattern4()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // 指定された座標に着いた時に射撃の準備
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // 射撃開始までの時間
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // 射撃開始してから時間経過後に射撃を止める
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position -= Vector3.up * enemySpeed;
            // 時間経過で敵を削除する
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }
    // 指定した位置に移動したのちに時間が経過したらY座標に向かって移動後消滅させる
    private IEnumerator EnemyMovePattern5()
    {
        if (!enemyCheck)
        {
            Vector3 enemySetPos = new Vector3(enemySetPosX, enemySetPosY, enemySetPosZ);
            transform.position = Vector3.MoveTowards(transform.position, enemySetPos, enemySpeed);

            // 指定された座標に着いた時に射撃の準備
            if (Mathf.Floor(this.transform.position.z) == enemySetPosZ)
            {
                // 射撃開始までの時間
                yield return new WaitForSeconds(shotStartTime);

                if (bulletTimeElapsed >= enemyBulletInterval)
                {
                    var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
                    shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
                    bulletTimeElapsed = 0.0f;
                }

                // 射撃開始してから時間経過後に射撃を止める
                yield return new WaitForSeconds(timeElapsed);
                enemyCheck = true;
            }
        }
        else
        {
            transform.position += Vector3.up * enemySpeed;
            // 時間経過で敵を削除する
            yield return new WaitForSeconds(destroyTime);
            Destroy(this.gameObject);
        }
    }


    // 横にジグザグや反射系などの動きなどに使用
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

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 縦にジグザグや反射系などの動きなどに使用
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

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 右に向かう滑らかな曲線移動
    private IEnumerator EnemyMovePattern8()
    {
        timeElapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Mathf.Sin(timeElapsed) * amplitude, transform.position.y, transform.position.z);
        transform.position -= Vector3.forward * enemySpeed;

        yield return new WaitForSeconds(shotStartTime);

        // 敵の弾の発射と発射間隔の処理
        if (bulletTimeElapsed >= enemyBulletInterval)
        {
            var shotBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            shotBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * enemyBulletSpeed;
            bulletTimeElapsed = 0.0f;
        }

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 左に向かう滑らかな曲線移動
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

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 上に向かう滑らかな曲線移動
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

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    // 下に向かう滑らかな曲線移動
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

        // 時間経過で敵を削除する
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}

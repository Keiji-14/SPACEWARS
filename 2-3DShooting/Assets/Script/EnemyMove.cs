using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int enemyPatternNum;
    public int enemyHp;
    public float enemySpeed;
    public float amplitude;
    public float enemyBulletSpeed;
    public float enemyBulletInterval;
    public float shotStartTime;
    public float timeElapsed;
    public float destroyTime;

    private int score;
    private float bulletTimeElapsed;
    public bool enemyCheck = false;
    public bool rendered = false;
    public bool hitCollider = false;

    private const string MAIN_CAMERA_TAG_NAME = "MainCamera";

    [SerializeField] GameObject target;
    [SerializeField] GameObject enemyBullet;

    [Header("Item")]
    [SerializeField] GameObject recoveryItem;

    [Header("SetPosition")]
    [SerializeField] float enemySetPosX;
    [SerializeField] float enemySetPosY;
    [SerializeField] float enemySetPosZ;

    [Header("Expload")]
    [SerializeField] int exploadNum;
    [SerializeField] float exploadScaleX;
    [SerializeField] float exploadScaleY;
    [SerializeField] float exploadScaleZ;
    [SerializeField] ParticleSystem[] exploadEffect;

    [Header("SE")]
    [SerializeField] AudioClip destroySE;

    private enum Enemy
    {
        enemy1_1,
        enemy1_2,
        enemy1_3,
        enemy1_4,
        enemy1_5,
        enemy1_6,
        enemy1_7,
        enemy1_8,
        enemy1_9,
        enemy1_10,
        enemy1_11,
        enemy2,
        enemy3_1,
        enemy3_2,
    }

    void Update()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
    }

    void FixedUpdate()
    {
        bulletTimeElapsed += Time.deltaTime;

        // 敵がプレイヤーの方向に向く処理
        transform.LookAt(target.transform);

        if (rendered)
        {
            hitCollider = true;
            EnemyMovePattern();
        }
        else
        {
            transform.position += Vector3.forward * -0.02f;
        }

        rendered = false;
    }

    // カメラに移ったかの確認処理
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ Rendered を有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            rendered = true;
        }
    }

    // やられた時の消滅する処理
    private void Expload()
    {
        for (int i = 0; i < exploadNum; i++)
        {
            exploadEffect[i].transform.localScale = new Vector3(exploadScaleX * 0.01f, exploadScaleY * 0.01f, exploadScaleZ * 0.01f);
            Instantiate(exploadEffect[i], transform.position, Quaternion.identity);
        }
        AudioSource.PlayClipAtPoint(destroySE, transform.position);
    }

    
    private void CrushEnemy()
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

        // 敵の種類によって獲得するスコアが変動
        switch (enemyPatternNum)
        {
            case (int)Enemy.enemy2:
                score += 100;
                break;
            case (int)Enemy.enemy3_1:
                score += 2000;
                break;
            case (int)Enemy.enemy3_2:
                score += 2000;
                break;
            default:
                score += 200;
                break;
        }
    }

    // 敵の当たり判定の設定と処理
    public void OnCollisionEnter(Collision collision)
    {
        if (hitCollider)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                enemyHp -= 1;
                if (enemyHp <= 0)
                {
                    Expload();
                    Destroy(this.gameObject);
                    CrushEnemy();
                    
                }
                PlayerPrefs.SetInt("SCORE", score);
            }
            // 特攻型の敵だった場合はプレイヤーにぶつかったら自爆する
            if (enemyPatternNum == 11)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Expload();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void EnemyMovePattern()
    {
        switch(enemyPatternNum)
        {
            case (int)Enemy.enemy1_1:
                StartCoroutine(Pattern1());
                break;
            case (int)Enemy.enemy1_2:
                StartCoroutine(Pattern2());
                break;
            case (int)Enemy.enemy1_3:
                StartCoroutine(Pattern3());
                break;
            case (int)Enemy.enemy1_4:
                StartCoroutine(Pattern4());
                break;
            case (int)Enemy.enemy1_5:
                StartCoroutine(Pattern5());
                break;
            case (int)Enemy.enemy1_6:
                StartCoroutine(Pattern6());
                break;
            case (int)Enemy.enemy1_7:
                StartCoroutine(Pattern7());
                break;
            case (int)Enemy.enemy1_8:
                StartCoroutine(Pattern8());
                break;
            case (int)Enemy.enemy1_9:
                StartCoroutine(Pattern9());
                break;
            case (int)Enemy.enemy1_10:
                StartCoroutine(Pattern10());
                break;
            case (int)Enemy.enemy1_11:
                StartCoroutine(Pattern11());
                break;
            case (int)Enemy.enemy2:
                StartCoroutine(Pattern12());
                break;
            case (int)Enemy.enemy3_1:
                StartCoroutine(Pattern13());
                break;
            case (int)Enemy.enemy3_2:
                StartCoroutine(Pattern14());
                break;
        }
    }

    // 指定した位置に移動したのちに時間が経過したら-Z座標に向かって移動後消滅させる
    private IEnumerator Pattern1()
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
    private IEnumerator Pattern2()
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
    private IEnumerator Pattern3()
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
    private IEnumerator Pattern4()
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
    private IEnumerator Pattern5()
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
    private IEnumerator Pattern6()
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
    private IEnumerator Pattern7()
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
    private IEnumerator Pattern8()
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
    private IEnumerator Pattern9()
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
    private IEnumerator Pattern10()
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
    private IEnumerator Pattern11()
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

    // 特攻して来て自爆してくる敵の処理
    private IEnumerator Pattern12()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed);

        if (transform.position == target.transform.position)
        {
            Expload();
            Destroy(this.gameObject);
        }

        yield return new WaitForSeconds(destroyTime);
        Expload();
        Destroy(this.gameObject);
    }

    // 大型の敵(フェーズ1)の処理
    private IEnumerator Pattern13()
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
    private IEnumerator Pattern14()
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

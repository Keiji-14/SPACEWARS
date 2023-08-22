using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerSetting")]
    public int hp;
    public float speed;
    public float shootInterval;
    public float loopCount;
    public float flashInterval;

    private int phaseNum;
    private float timeElapsed;

    [Header("HPBar")]
    [SerializeField] GameObject[] hpBar;

    [Header("JetFire")]
    [SerializeField] GameObject centetFire;
    [SerializeField] GameObject leftFire;
    [SerializeField] GameObject rightFire;

    [Header("Bullet・MuzzleFlash")]
    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem leftMuzzleFlash;
    [SerializeField] ParticleSystem rightMuzzleFlash;

    [Header("Expload")]
    [SerializeField] int exploadNum;
    [SerializeField] float exploadScaleX;
    [SerializeField] float exploadScaleY;
    [SerializeField] float exploadScaleZ;
    [SerializeField] ParticleSystem[] exploadEffect;

    [Header("SE")]
    [SerializeField] AudioClip shootSE;
    [SerializeField] AudioClip recoverySE;
    [SerializeField] AudioClip damageSE;
    [SerializeField] AudioClip destroySE;

    Vector3 setInitPhasePos1;
    Vector3 setInitPhasePos2;
    Vector3 setInitPhasePos3;

    [Header("GetComponent")]
    public DelayProcessing delayProcessing;
    public Pause pause;

    AudioSource audioSource;

    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        phaseNum = PlayerPrefs.GetInt("PHASE", 0);

        // 衝突した時の座標ずれを修正するための初期座標設定
        setInitPhasePos1 = new Vector3(transform.position.x, 0.0f, transform.position.z);
        setInitPhasePos2 = new Vector3(0.0f, transform.position.y, transform.position.z);
        setInitPhasePos3 = new Vector3(transform.position.x, transform.position.y, 0.0f);

        // プレイヤーの操作関係
        if (!pause.pauseNow)
        {
            PlayerMove();
            PlayerArea();
            PlayerShoot();
        }

        // プレイヤーの体力関係
        PlayerHP();
    }

    // プレイヤーの動き
    private void PlayerMove()
    {
        // 入力を取得
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        switch (phaseNum)
        {
            case 0:
                transform.position = setInitPhasePos1;

                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    transform.position += new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
                }
                if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    transform.position -= new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
                }
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    transform.position -= new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
                }
                break;
            case 1:
                transform.position = setInitPhasePos2;

                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    transform.position += new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    transform.position -= new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    transform.position += new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    transform.position -= new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
                }
                break;
            case 2:
                transform.position = setInitPhasePos3;

                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    transform.position += new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    transform.position -= new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    transform.position -= new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
                }
                break;
        }
    }

    // プレイヤーが画面外に出ないように制御する処理
    private void PlayerArea()
    {
        switch (phaseNum)
        {
            case 0:
                if (transform.position.z > 11.0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 11.0f);
                }
                if (transform.position.z < -1.0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
                }
                if (transform.position.x < -12.0f)
                {
                    transform.position = new Vector3(-12.0f, transform.position.y, transform.position.z);
                }
                if (transform.position.x > 12.0f)
                {
                    transform.position = new Vector3(12.0f, transform.position.y, transform.position.z);
                }
                break;
            case 1:
                if (transform.position.y > 6.0f)
                {
                    transform.position = new Vector3(transform.position.x, 6.0f, transform.position.z);
                }
                if (transform.position.y < -6.0f)
                {
                    transform.position = new Vector3(transform.position.x, -6.0f, transform.position.z);
                }
                if (transform.position.z < -6.0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, -6.0f);
                }
                if (transform.position.z > 16.5f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 16.5f);
                }
                break;
            case 2:
                if (transform.position.y > 3.0f)
                {
                    transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
                }
                if (transform.position.y < -2.5f)
                {
                    transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
                }
                if (transform.position.x < -5.0f)
                {
                    transform.position = new Vector3(-5.0f, transform.position.y, transform.position.z);
                }
                if (transform.position.x > 5.0f)
                {
                    transform.position = new Vector3(5.0f, transform.position.y, transform.position.z);
                }
                break;
        }
    }

    // プレイヤーの体力関係の処理
    private void PlayerHP()
    {
        hp = PlayerPrefs.GetInt("PLAYERHP", 0);

        // HPが5(最大)だった場合に超えないように設定
        if (hp > 5)
        {
            hp = 5;
        }

        switch (hp)
        {
            case 0:
                GameOver();
                hpBar[0].SetActive(false);
                hpBar[1].SetActive(false);
                hpBar[2].SetActive(false);
                hpBar[3].SetActive(false);
                hpBar[4].SetActive(false);
                break;
            case 1:
                hpBar[0].SetActive(true);
                hpBar[1].SetActive(false);
                hpBar[2].SetActive(false);
                hpBar[3].SetActive(false);
                hpBar[4].SetActive(false);
                break;
            case 2:
                hpBar[0].SetActive(true);
                hpBar[1].SetActive(true);
                hpBar[2].SetActive(false);
                hpBar[3].SetActive(false);
                hpBar[4].SetActive(false);
                break;
            case 3:
                hpBar[0].SetActive(true);
                hpBar[1].SetActive(true);
                hpBar[2].SetActive(true);
                hpBar[3].SetActive(false);
                hpBar[4].SetActive(false);
                break;
            case 4:
                hpBar[0].SetActive(true);
                hpBar[1].SetActive(true);
                hpBar[2].SetActive(true);
                hpBar[3].SetActive(true);
                hpBar[4].SetActive(false);
                break;
            case 5:
                hpBar[0].SetActive(true);
                hpBar[1].SetActive(true);
                hpBar[2].SetActive(true);
                hpBar[3].SetActive(true);
                hpBar[4].SetActive(true);
                break;
        }
    }

    // プレイヤーの体力が回復する時の処理
    public void PlayerLifeRecovery()
    {
        hp += 1;
        PlayerPrefs.SetInt("PLAYERHP", hp);

        audioSource.PlayOneShot(recoverySE);
    }

    // プレイヤーがダメージを受けた時の処理
    public void PlayerDamage()
    {
        hp -= 1;
        PlayerPrefs.SetInt("PLAYERHP", hp);

        audioSource.PlayOneShot(damageSE);
        StartCoroutine(DamageBlink());
    }

    // 弾の処理
    private void PlayerShoot()
    {
        // 弾の出現位置の調整
        Vector3 leftBullet = new Vector3(transform.position.x - 0.3f, transform.position.y - 0.3f, transform.position.z + 0.4f);
        Vector3 rightBullet = new Vector3(transform.position.x + 0.3f, transform.position.y - 0.3f, transform.position.z + 0.4f);

        if (Input.GetKey("space"))
        {
            if (timeElapsed >= shootInterval)
            {
                leftMuzzleFlash.Play();
                rightMuzzleFlash.Play();
                audioSource.PlayOneShot(shootSE);

                Instantiate(bullet, leftBullet, Quaternion.identity);
                Instantiate(bullet, rightBullet, Quaternion.identity);
                timeElapsed = 0.0f;
            }
        }
    }

    private IEnumerator DamageBlink()
    {
        //点滅ループ開始
        for (int i = 0; i < loopCount; i++)
        {
            GetRenderer();

            yield return new WaitForSeconds(flashInterval * 0.1f);
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        Expload();
        delayProcessing.gameOverCheck = true;
        Destroy(this.gameObject);
    }

    // 実機がやられた時の消滅エフェクト
    private void Expload()
    {
        for (int i = 0; i < exploadNum; i++)
        {
            exploadEffect[i].transform.localScale = new Vector3(exploadScaleX * 0.01f, exploadScaleY * 0.01f, exploadScaleZ * 0.01f);
            Instantiate(exploadEffect[i], transform.position, Quaternion.identity);
        }
        audioSource.PlayOneShot(destroySE);
    }

    // プレイヤーを点滅状態にする処理
    private void GetRenderer()
    {
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        centetFire.GetComponent<Renderer>().enabled = !centetFire.GetComponent<Renderer>().enabled;
        leftFire.GetComponent<Renderer>().enabled = !leftFire.GetComponent<Renderer>().enabled;
        rightFire.GetComponent<Renderer>().enabled = !rightFire.GetComponent<Renderer>().enabled;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 敵に衝突した時の処理
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.layer = LayerMask.NameToLayer("Damaging");
            PlayerDamage();
        }

        // 敵の弾に衝突した時の処理
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            gameObject.layer = LayerMask.NameToLayer("Damaging");
            PlayerDamage();

        }

        // 隕石に衝突した時の処理
        if (collision.gameObject.CompareTag("Meteor"))
        {
            gameObject.layer = LayerMask.NameToLayer("Damaging");
            PlayerDamage();
        }
    }
}

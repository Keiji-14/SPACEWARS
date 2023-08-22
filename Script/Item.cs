using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject player_object;
    Player player;
    
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        player = player_object.GetComponent<Player>();
    }

    void FixedUpdate()
    {
        transform.position += Vector3.forward * -0.02f;

        Invoke("DestroyItem", 30.0f);
    }

    private void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに衝突した時の処理
        if (collision.gameObject.CompareTag("Player"))
        {
            player.PlayerLifeRecovery();
            Destroy(this.gameObject);
        }

        // アイテムを消す壁に衝突した時にアイテムを削除する
        if (collision.gameObject.CompareTag("Object Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}

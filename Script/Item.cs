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
        // �v���C���[�ɏՓ˂������̏���
        if (collision.gameObject.CompareTag("Player"))
        {
            player.PlayerLifeRecovery();
            Destroy(this.gameObject);
        }

        // �A�C�e���������ǂɏՓ˂������ɃA�C�e�����폜����
        if (collision.gameObject.CompareTag("Object Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}

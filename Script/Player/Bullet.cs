using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    [SerializeField] ParticleSystem bulletHitEffect;

    void Start()
    {
        transform.Rotate(90.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, bulletSpeed) * Time.deltaTime;

        Invoke("DelayDestroy", 10.0f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // �G�ɓ��������e���폜����
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject); 
        }

        // 覐΂ɓ��������e���폜����
        if (collision.gameObject.CompareTag("Meteor"))
        {
            Instantiate(bulletHitEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }
    private void DelayDestroy()
    {
        Destroy(this.gameObject);
    }
}

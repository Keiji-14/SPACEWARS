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
        // ƒvƒŒƒCƒ„[‚ÉÕ“Ë‚µ‚½‚É’e‚ğíœ‚·‚é
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        // ’e‚ğÁ‚·•Ç‚ÉÕ“Ë‚µ‚½‚É’e‚ğíœ‚·‚é
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

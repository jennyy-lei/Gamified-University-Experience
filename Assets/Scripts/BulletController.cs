using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int dmg;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        rb2d.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Enemy enemy = hitInfo.GetComponent<Enemy>();
        // if (enemy != null) {
        //     enemy.TakeDamage(dmg );
        // }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int dmg;
    public Animator animator;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        rb2d.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "PlayerAtkIgnore") {
            return;
        }

        animator.SetBool("hit", true);
        // Enemy enemy = hitInfo.GetComponent<Enemy>();
        // if (enemy != null) {
        //     enemy.TakeDamage(dmg );
        // }

        animator.Update(0);

        Destroy (gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        rb2d.velocity = new Vector2(0 ,0);
    }
}

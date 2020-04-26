using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float knockbackForce;
    public float maxDist;
    public int dmg{get;set;}
    public Animator animator;

    private Rigidbody2D rb2d;
    private float travelDist;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        travelDist = 0;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag(StrConstant.playerTag)) {
            return;
        }

        animator.SetBool("hit", true);
        Enemy2 enemy = hitInfo.GetComponentInParent<Enemy2>();
        if (enemy != null) {
            enemy.remainHealth -= dmg;
        }

        animator.Update(0);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        rb2d.velocity = new Vector2(0 ,0);
    }

    void Update(){
        travelDist += speed*Time.deltaTime;
        if(ReachedBoundary() || travelDist >= maxDist){
            Destroy(gameObject, 0.1f);
        }
    }

    private bool ReachedBoundary(){
        float x = Camera.main.WorldToViewportPoint (transform.position).x;
        return x >= 1 || x <= 0;
    }
}

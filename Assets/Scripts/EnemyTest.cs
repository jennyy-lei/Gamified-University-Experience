using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float walkDist;
    public Animator animator;
    private Rigidbody2D rb2d;
    private float walkedDist = 0;

    private bool faceRight = true;
    // Start is called before the first frame update
    void start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed",speed);
        transform.position += new Vector3(((faceRight) ? 1 : -1)*speed*Time.deltaTime,0f,0f);
        walkedDist += speed*Time.deltaTime;
        if(walkedDist >= walkDist){
            flip();
            walkedDist = 0;
        }

        
    }
        
    private void flip()
	{
		// Switch the way the player is labelled as facing.
		faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
	}
}

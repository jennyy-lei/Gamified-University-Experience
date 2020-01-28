using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public bool isGrounded = true;
    public Animator animator;

    private Rigidbody2D rb2d;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() ;
    }

    // Update is called once per frame
    void Update()
    {
        jump();
        move();
    }

    private void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));

        transform.position += movement * Time.deltaTime;

        // If the input is moving the player right and the player is facing left...
        if (moveHorizontal > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (moveHorizontal < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);

            // isGrounded = false;
        }
    }
}

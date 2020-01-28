using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    private float speed;
    private float jumpHeight;
    private Animator animator;
    private Rigidbody2D rb2d;
    private Transform unitPos;
    private bool isGrounded = true;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    public MoveController(float _speed, float _jumpHeight, Animator _animator, Rigidbody2D _rb2d, Transform _unitPos)
    {
        speed = _speed;
        jumpHeight = _jumpHeight;
        animator = _animator;
        rb2d = _rb2d;
        unitPos = _unitPos;
    }

    public void updateMove()
    {
        jump();
        move();
    }

    private void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        animator.SetFloat("speed", Mathf.Abs(movement.x));

        unitPos.position += movement * Time.deltaTime;

        // If the input is moving the player right and the player is facing left...
        if (movement.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movement.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

        unitPos.Rotate(0f, 180f, 0f);
	}

    private void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);

            // isGrounded = false;
        }
    }
}

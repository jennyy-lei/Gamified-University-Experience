using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController
{
    private Animator animator = null;
    private Rigidbody2D rb2d = null;
    private Transform unitPos = null;
    private bool isGrounded = true;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    
    public void init(Animator _animator, Rigidbody2D _rb2d, Transform _unitPos)
    {
        animator = _animator;
        rb2d = _rb2d;
        unitPos = _unitPos;
    }

    public void setUnitPos(Transform _unitPos) {
        unitPos = _unitPos;
    }

    public Transform getUnitPos() => unitPos;

    public void toggleDirection() {
        m_FacingRight = !m_FacingRight;
    }

    public bool getIsFacingRight() => m_FacingRight;

    public void moveHorizontal(float movement, bool cond1, bool cond2 = false)
    {
        Vector3 v = new Vector3(movement, 0f, 0f);

        unitPos.position += v * Time.deltaTime;

        FlipCondition(movement, cond1, cond2);
    }

    private void FlipCondition(float movement, bool cond1, bool cond2) {
        // If the input is moving the player right and the player is facing left...
        if (cond1)
        {
            // ... flip the player.
            FlipSprite();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (cond2)
        {
            // ... flip the player.
            FlipSprite();
        }
    }

    public virtual void FlipSprite()
	{
		// Switch the way the player is labelled as facing.
		toggleDirection();

        unitPos.Rotate(0f, 180f, 0f);
	}

    public void jump(float jumpHeight)
    {
        rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);

            // isGrounded = false;
    }
}

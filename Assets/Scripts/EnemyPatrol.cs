using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MoveController
{
    public EnemyPatrol(Animator _animator, Rigidbody2D _rb2d, Transform _unitPos)
    {
        init(_animator, _rb2d, _unitPos);
    }
    public void move(float movement, bool cond)
    {
        moveHorizontal(movement, cond);
    }

    public override void FlipSprite()
	{
		// Switch the way the player is labelled as facing.
		toggleDirection();

        getUnitPos().GetChild(0).gameObject.transform.Rotate(0f, 180f, 0f);
	}
}

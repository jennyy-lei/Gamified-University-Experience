using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MoveController
{   
    public PlayerMoveController(Animator _animator, Rigidbody2D _rb2d, Transform _unitPos)
    {
        init(_animator, _rb2d, _unitPos);
    }
    public void move(float movement)
    {
        moveHorizontal(movement, movement > 0 && !getIsFacingRight(), movement < 0 && getIsFacingRight());
    }
}

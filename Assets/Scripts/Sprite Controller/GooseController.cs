using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooseController : MonoBehaviour
{
    private Goose info;
    private Transform groundDetection;
    private Animator playerAnim;

    private Command moveCmd;
    void Awake()
    {
        groundDetection = transform.GetChild(0).GetChild(0);
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Goose>();
        info.walkSpeed = info.MAX_WALK_SPEED;
        moveCmd = new MoveCmd(playerAnim,transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
       RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
       if(!info.facingRight) info.walkSpeed = -info.MAX_WALK_SPEED;
       else info.walkSpeed = info.MAX_WALK_SPEED;
       if(!groundInfo.collider){
           info.walkSpeed *= -1;
       } 
       moveCmd.execute(transform,info);
    }
}

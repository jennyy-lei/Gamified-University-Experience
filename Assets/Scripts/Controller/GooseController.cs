using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooseController : MonoBehaviour
{
    private Goose info;
    private Transform groundDetection;
    private Animator playerAnim;

    private Command walkCmd;
    private Command dashCmd;
    void Awake()
    {
        groundDetection = transform.GetChild(0).GetChild(0);
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Goose>();
        info.walkSpeed = info.MAX_WALK_SPEED;
        walkCmd = new MoveCmd(transform.GetChild(0));
        dashCmd = new DashCmd();
    }

    // Update is called once per frame
    void Update()
    {
        walk(info);
    }

    void walk(IDashable dashInfo){
        Command moveCmd;
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
        info.walkSpeed = dashInfo.isDashing ? dashInfo.dashSpeed : info.MAX_WALK_SPEED;
        info.walkSpeed *= info.facingRight ? 1 : -1;
        if(!groundInfo.collider){
            info.rb2d.velocity = Vector2.zero;
            info.walkSpeed *= -1;
        }
        moveCmd = dashInfo.isDashing ? dashCmd : walkCmd;
        moveCmd.execute(transform,info);
    }
}

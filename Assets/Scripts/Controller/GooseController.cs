using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooseController : EnemyController
{
    private Goose info;
    private Transform groundDetection;
    private Animator playerAnim;

    private Command walkCmd;
    private Command dashCmd;

    private bool dashRight;

    public LayerMask layerMask;
    void Awake()
    {
        groundDetection = transform.GetChild(0).GetChild(0);
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Goose>();
        info.walkSpeed = info.MAX_WALK_SPEED;
        walkCmd = new MoveCmd(transform.GetChild(0));
        dashCmd = new DashCmd(transform.GetChild(0));
    }

    // Update is called once per frame
    void Update()
    {
        if(((IDashable) info).isDashing){
            dash(info);
        }
        else{
            walk();
        }
    }
    public override void triggerAggro(){
        dashRight = ((IDashable) info).dashTargetX < transform.position.x;
        ((IDashable) info).isDashing = true;
    }
    void walk(){
        info.walkSpeed = info.MAX_WALK_SPEED;
        info.walkSpeed *= info.facingRight ? 1 : -1;
        if(!isGround()){
            info.rb2d.velocity = Vector2.zero;
            info.walkSpeed *= -1;
        }
        walkCmd.execute(transform,info);
    }

    void dash(IDashable dashInfo){
        info.walkSpeed = dashInfo.dashSpeed;
        info.walkSpeed *= dashRight ? -1 : 1;
        if(!isGround()){
            dashInfo.isDashing = false;
            return;
        }
        dashCmd.execute(transform,info);
    }

    bool isGround(){
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, layerMask);
        return groundInfo.collider;
    }
}

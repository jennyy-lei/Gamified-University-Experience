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
    private float dashDist;

    private bool canDash;
    public float dashCD;

    public LayerMask groundLayerMask;
    void Awake()
    {
        groundDetection = transform.GetChild(0).GetChild(0);
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Goose>();
        info.moveSpeed = info.MAX_WALK_SPEED;
        walkCmd = new MoveCmd(transform.GetChild(0));
        dashDist = 0;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(dashDist > 0){
            dash();
        }
        else{
            walk();
        }
    }
    void walk(){
        info.moveSpeed = info.MAX_WALK_SPEED;
        info.moveSpeed *= info.facingRight ? 1 : -1;
        if(!isGround()){
            info.rb2d.velocity = Vector2.zero;
            info.moveSpeed *= -1;
        }
        walkCmd.execute(transform,info);
    }
    void dash()
    {
        if(!isGround()){
            dashDist = 0;
            info.rb2d.velocity = Vector2.zero;
            info.moveSpeed *= -1;
            return;
        }
        dashDist -= Mathf.Abs(info.moveSpeed)*Time.deltaTime;
        walkCmd.execute(transform,info);
    } 
    void dmgKnockback(Transform source,float force, int dmg){
        info.remainHealth -= dmg;
        force = source.position.x < transform.position.x ? force : -force;
        info.rb2d.velocity = Vector2.zero;
        info.rb2d.AddForce(new Vector2(force,0),ForceMode2D.Impulse);
    }
    public override void triggerAggro(Transform target){
        IDashable dashInfo = (IDashable) info;
        dashDist = dashInfo.dashDist;
        info.moveSpeed = dashInfo.dashSpeed;
        info.moveSpeed *= target.position.x > transform.position.x ? 1 : -1;
    }

    bool isGround(){
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, groundLayerMask);
        return groundInfo.collider;
    }

    void OnTriggerEnter2D (Collider2D hitInfo){
        if(hitInfo.gameObject.CompareTag(StrConstant.bulletTag)){
            hitInfo.enabled = false;
            BulletController bc = hitInfo.GetComponent<BulletController>();
            dmgKnockback(hitInfo.transform,bc.knockbackForce,bc.dmg);
        }
        if(hitInfo.gameObject.CompareTag(StrConstant.meleeTag)){
            Weapon w = hitInfo.GetComponentInParent<Weapon>();
            dmgKnockback(hitInfo.transform,w.meleeKnockback,w.meleeDmg);
        }
    }

    void OnTriggerStay2D (Collider2D hitInfo){
        if(canDash && hitInfo.gameObject.CompareTag(StrConstant.playerTag)){
            canDash = false;
            triggerAggro(hitInfo.transform);
            Invoke("resetDashCD",dashCD);
        }
    }
    private void resetDashCD() => canDash = true;

}

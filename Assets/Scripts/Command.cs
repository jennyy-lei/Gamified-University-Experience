using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
All available Command class, use Ctrl+F wisely!

1. Movement type:
    - MoveCmd
    - JumpCmd
2. Attack Type:
    -ShootCmd
    
*/

public abstract class Command
{
    public string name{
        get;
    }

    public Command(string name){
        this.name = name;
    }

    public abstract void execute(Transform character, Unit2 info);

    public void setAnimFloat(Animator animator, string name,float val){
        if(animator != null){
            animator.SetFloat(name, val);
        }
    }
}

public class TemplateCmd : Command{
    public TemplateCmd() : base("Template")
    {
        
    }

    public override void execute(Transform character, Unit2 info){
        //..
    }
}

public class MoveCmd : Command{
    private Transform flipPos;
    public MoveCmd(Transform flipPos = null) : base("Move")
    {
        this.flipPos = flipPos;
    }

    public override void execute(Transform character, Unit2 info){
        Vector3 v = new Vector3(info.walkSpeed, 0f, 0f);
        if((info.facingRight && info.walkSpeed < 0) || (!info.facingRight && info.walkSpeed > 0)){
            if(flipPos == null) {
                character.Rotate(0f,180f,0f);
            }else{
                flipPos.Rotate(0f,180f,0f);
            }
            info.facingRight = !info.facingRight;
        }
        character.position += v * Time.deltaTime;
        setAnimFloat(info.animator, "speed", Mathf.Abs(info.walkSpeed));
    }
}

public class JumpCmd : Command{
    public JumpCmd() : base("Jump")
    {

    }

    public override void execute(Transform character, Unit2 info){
        IJumpable jumpInfo = (IJumpable) info;
        info.rb2d.AddForce(new Vector2(0f, jumpInfo.jumpPow), ForceMode2D.Impulse);
    }
}

public class ShootCmd : Command{

    public ShootCmd() : base("Shoot")
    {

    }

    public override void execute(Transform character, Unit2 info){        
        IShootable shootInfo = (IShootable) info;

        if (shootInfo.bulletCount == 0) return;

        Transform firePoint = shootInfo.shootPos;
        GameObject.Instantiate(shootInfo.bullet, firePoint.position, firePoint.rotation);
        shootInfo.bulletCount--;
    }
}

public class DashCmd : Command{

    private MoveCmd moveCmd;
    private float dashedDist;

    public DashCmd(Transform flipPos = null) : base("Dash")
    {
        dashedDist = 0;
        moveCmd = new MoveCmd(flipPos);

    }

    public override void execute(Transform character, Unit2 info){
        IDashable dashInfo = (IDashable) info; 
        if(dashedDist >= dashInfo.dashDist){
            dashedDist = 0;
            dashInfo.isDashing = false;
            return;
        }
        dashedDist += dashInfo.dashSpeed * Time.deltaTime;
        moveCmd.execute(character,info);
    }
}
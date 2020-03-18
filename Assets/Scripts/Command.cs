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
    protected Animator animator;

    public string name{
        get;
    }

    public Command(Animator anim,string name){
        this.animator = anim;
        this.name = name;
    }

    public abstract void execute(Transform character, Unit2 info);

    public void setAnimFloat(string name,float val){
        if(animator != null){
            animator.SetFloat(name, val);
        }
    }
}

public class TemplateCmd : Command{
    public TemplateCmd() : base(null,"Template")
    {
        
    }

    public override void execute(Transform character, Unit2 info){
        //..
    }
}

public class MoveCmd : Command{
    Transform flipPos;
    public MoveCmd(Animator anim,Transform flipPos = null) : base(anim,"Move")
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
        setAnimFloat("speed",Mathf.Abs(info.walkSpeed));
    }
}

public class JumpCmd : Command{
    private Rigidbody2D rb2d;
    public JumpCmd(Animator anim, Rigidbody2D rb2d) : base(anim,"Jump")
    {
        this.rb2d = rb2d;
        
    }

    public override void execute(Transform character, Unit2 info){
        IJumpable jumpInfo = (IJumpable) info;
        rb2d.AddForce(new Vector2(0f, jumpInfo.jumpPow), ForceMode2D.Impulse);
    }
}

public class ShootCmd : Command{

    public ShootCmd(Animator anim) : base(anim,"Shoot")
    {

    }

    public override void execute(Transform character, Unit2 info){        
        IShootable shootInfo = (IShootable) info;

        if (shootInfo.bulletCount == 0) return;

        Transform firePoint = character.transform.GetChild(0);
        GameObject.Instantiate(shootInfo.bullet, firePoint.position, firePoint.rotation);
        shootInfo.bulletCount--;
    }
}
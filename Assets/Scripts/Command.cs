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

    public MoveCmd() : base("Move")
    {

    }

    public override void execute(Transform character, Unit2 info){
        Vector3 v = new Vector3(info.walkSpeed, 0f, 0f);
        character.position += v * Time.deltaTime;
    }
}

public class JumpCmd : Command{
    public JumpCmd() : base("Jump")
    {
        
    }

    public override void execute(Transform character, Unit2 info){
        Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
        IJumpable jumpInfo = (IJumpable) info;
        rb2d.AddForce(new Vector2(0f, jumpInfo.jumpPow), ForceMode2D.Impulse);
    }
}

public class ShootCmd : Command{

    public ShootCmd(GameObject bullet) : base("Shoot")
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
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

    public abstract void execute(GameObject character);

}

public class TemplateCmd : Command{
    public TemplateCmd() : base("Template")
    {
        
    }

    public override void execute(GameObject character){
        Unit info = character.GetComponent<Unit>();
        //..
    }
}

public class MoveCmd : Command{

    public MoveCmd() : base("Move")
    {

    }

    public override void execute(GameObject character){
        Transform pos = character.GetComponent<Transform>();
        Unit info = character.GetComponent<Unit>();

        if (pos == null) throw new System.ArgumentException("Gameobject does not have Transform Component.", "character");
        if (info == null) throw new System.ArgumentException("Gameobject does not have Transform Component.", "character");
    }
}

public class JumpCmd : Command{
    public JumpCmd() : base("Jump")
    {
        
    }

    public override void execute(GameObject character){
        Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
        Unit info = character.GetComponent<Player>();

        if (rb2d == null) throw new System.ArgumentException("Gameobject does not have Rigidbody2D Component.", "character");
        if (info == null) throw new System.ArgumentException("Gameobject does not have Transform Component.", "character");
    
        rb2d.AddForce(new Vector2(0f, 5), ForceMode2D.Impulse);
    }
}

public class ShootCmd : Command{
    public ShootCmd() : base("Shoot")
    {

    }

    public override void execute(GameObject character){
        Transform pos = character.GetComponent<Transform>();
        Unit info = character.GetComponent<Unit>();
        
        if (pos == null) throw new System.ArgumentException("Gameobject does not have Transform Component.", "character");
        if (info == null) throw new System.ArgumentException("Gameobject does not have Transform Component.", "character");
    }
}



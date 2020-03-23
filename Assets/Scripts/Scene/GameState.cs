using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public float remainHealth;
    public float bulletCount;
    public bool facingRight;
    public int gold;

    public PlayerState(){
        this.remainHealth = 0;
        this.bulletCount = 0;
        this.gold = 0;
        this.facingRight = true;
    }

}

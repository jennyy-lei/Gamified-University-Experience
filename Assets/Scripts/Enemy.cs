using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    public Image healthBar;

    public override void animationEffects()
    {
        
    }
    public override void moveSprite()
    {

    }
    public override void attack()
    {

    }
    public override void deadZone()
    {

    }
    public override void updateHealthBar()
    {
        healthBar.fillAmount = getHealthRatio();
    }

    public override void destroy()
    {
        if (getRemainingHealth() <= 0) {
            Destroy (gameObject);
        }
    }
}

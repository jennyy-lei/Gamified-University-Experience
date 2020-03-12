using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit
{
    public Image healthBar;
    public float speed;
    public Transform groundDetection;

    private EnemyPatrol moveController;
    private bool a = true;

    public override void init()
    {
        moveController = new EnemyPatrol(null, null, transform);
    }

    public override void animationEffects()
    {
        
    }
    public override void moveSprite()
    {
        float m = speed;
        if (!moveController.getIsFacingRight())
            m = -speed;
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
        //Debug.Log(groundInfo.collider);
        moveController.move(m, !groundInfo.collider);

        healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
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

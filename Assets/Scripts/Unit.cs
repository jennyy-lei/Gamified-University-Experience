using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int remainingHealth = 10;
    private int totalHealth = 10;
    private int id = -1;
    private float atkDmg = 10;
    private float movementSpeed = 10;
    private bool isGrounded = true;
    private bool isDead = false;
    private bool isInit = false;

    public void init(int _health, int _id, int _atkDmg, float _speed)
    {
        remainingHealth = _health;
        totalHealth = _health;
        id = _id;
        atkDmg = _atkDmg;
        movementSpeed = _speed;

        isInit = true;
    }

    public int getRemainingHealth() => remainingHealth;
    public int getTotalHealth() => totalHealth;
    public float getHealthRatio() => remainingHealth / totalHealth;
    public int getId() => id;
    public float getAtkDmg() => atkDmg;
    public float getMovementSpeed() => movementSpeed;
    public bool getIsDead() => isDead;

    public void takeDmg(int h)
    {
        remainingHealth -= h;

        if (remainingHealth <= 0)
            isDead = true;
    }

    void Start()
    {
        init();
    }

    void Update()
    {
        animationEffects();
        moveSprite();
        attack();
        dead();
    }

    public virtual void init() {}
    public virtual void animationEffects() {}
    public virtual void moveSprite() {}
    public virtual void attack() {}
    public virtual void dead() {}
}

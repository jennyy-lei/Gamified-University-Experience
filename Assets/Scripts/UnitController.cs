using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController
{
    private int health;

    private int id;

    private float atkDmg;

    private float movementSpeed;

    private bool isGrounded = true;

    public UnitController(int _health, int _id, float _atkDmg, float _movementSpeed)
    {
        health = _health;
        id = _id;
        atkDmg = _atkDmg;
        movementSpeed = _movementSpeed;
    }

    public int getHealth() => health;
    public int getId() => id;
    public float getAtkDmg() => atkDmg;
    public float getMovementSpeed() => movementSpeed;
}

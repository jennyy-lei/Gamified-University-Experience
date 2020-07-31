using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : Enemy2,IDashable,IMelee
{
    //Dashable Property
    [field: SerializeField]
    public float dashDist {get;set;}
    [field: SerializeField]
    public float dashSpeed{get;set;}

    //Melee Property
    [field: SerializeField]
    public int meleeDmg{get;set;}
    public float meleeRange{get;set;}
    [field: SerializeField]
    public float meleeKnockback{get;set;}
    [field: SerializeField]
    public LayerMask enemyLayers{get;set;}
    public Transform hitPos{get;set;}

    protected override void initSpawn(){
        hitPos = GetComponent<Transform>();
    }
}

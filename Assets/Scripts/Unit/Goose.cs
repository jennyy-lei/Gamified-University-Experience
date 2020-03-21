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
    [field: SerializeField]
    public bool isDashing{get;set;}

    //Melee Property
    [field: SerializeField]
    public float meleeDmg{get;set;}
    public float meleeRange{get;set;}
    [field: SerializeField]
    public float knockbackForce{get;set;}

    protected override void initSpawn(){}
    void OnTriggerEnter2D (Collider2D hitInfo){
        string bulletTag = "Bullet";
        if(hitInfo.gameObject.CompareTag(bulletTag)){
            hitInfo.enabled = false;
            float force = hitInfo.gameObject.GetComponent<BulletController>().knockbackForce;
            bool left = (hitInfo.transform.position.x - transform.position.x) > 0;
            force = left ? -force : force;
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(force,0),ForceMode2D.Impulse);
        }
    }
}

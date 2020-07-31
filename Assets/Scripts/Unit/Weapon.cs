using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour,IMelee
{
    [SerializeField]
    private Animator animator;
    
    [field: SerializeField]
    public int dmg;

    [SerializeField]
    private float shootCD;
    [SerializeField]
    private float meleeCD;
    private bool canShoot;
    private bool canMelee;

    [field: SerializeField]
    public int meleeDmg {get;set;}
    [field: SerializeField]
    public float meleeRange {get;set;}
    [field: SerializeField]
    public float meleeKnockback {get;set;}
    [field: SerializeField]
    public Transform hitPos{set;get;}
    [field: SerializeField]
    public LayerMask enemyLayers{set;get;}

    void Awake(){
        hitPos.GetComponent<CircleCollider2D>().radius = meleeRange;
    }
    void Update(){
        hitPos.GetComponent<CircleCollider2D>().radius = meleeRange;
    }
    public bool Shoot()
    {
        if(canShoot) {            
            animator.SetTrigger("shoot");
            canShoot = false;
            Invoke("resetShootCD", shootCD);

            return true;
        }

        return false;
    }

    public bool MeleeAtk()
    {
        if(canMelee) {            
            animator.SetTrigger("melee");
            hitPos.GetComponent<Collider2D>().enabled=true;
            canMelee = false;
            Invoke("resetMeleeCD", meleeCD);
            return true;
        }

        return false;
    }
    public void endAnimation(){
        hitPos.GetComponent<Collider2D>().enabled=false;
    }

    private void resetMeleeCD() {
        canMelee = true;
    }
    private void resetShootCD() => canShoot = true;
    void Start()
    {
        canShoot = true;
        canMelee = true;
    }
}
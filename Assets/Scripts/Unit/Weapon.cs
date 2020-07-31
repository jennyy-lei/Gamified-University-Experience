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
            canMelee = false;
            Invoke("resetMeleeCD", meleeCD);
            Collider2D[] hits = Physics2D.OverlapCircleAll(hitPos.position, meleeRange,enemyLayers);
            Debug.Log(hits.Length);
            foreach(Collider2D hit in hits){
                Enemy2 e = hit.GetComponentInParent<Enemy2>();
                e.remainHealth -= meleeDmg;
                Vector2 dir = hitPos.position - hit.transform.position;
                dir = -dir.normalized;
                hit.attachedRigidbody.AddForce(dir*meleeKnockback,ForceMode2D.Impulse);
            }
            return true;
        }

        return false;
    }
    void OnDrawGizmosSelected(){
        if(hitPos==null) return;
        Gizmos.DrawWireSphere(hitPos.position, meleeRange);
    }

    private void resetMeleeCD() => canMelee = true;
    private void resetShootCD() => canShoot = true;
    void Start()
    {
        canShoot = true;
        canMelee = true;
    }
}
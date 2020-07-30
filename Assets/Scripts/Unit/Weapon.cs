using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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
            Invoke("resetMeleeCD", shootCD);

            return true;
        }

        return false;
    }

    private void resetMeleeCD() => canMelee = true;
    private void resetShootCD() => canShoot = true;
    void Start()
    {
        canShoot = true;
        canMelee = true;
    }
}
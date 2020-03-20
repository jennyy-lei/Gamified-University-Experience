using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float coolDown;
    private bool canShoot;

    public bool Attack()
    {
        if(canShoot) {            
            animator.SetBool("shooting", true);

            canShoot = false;
            Invoke("resetCoolDown", coolDown);

            return true;
        }

        return false;
    }

    private void resetCoolDown() => canShoot = true;

    void Start()
    {
        animator.SetBool("shooting", false);
        canShoot = true;
    }

    public void finishAnimation()
    {
        animator.SetBool("shooting", false);
    }
}

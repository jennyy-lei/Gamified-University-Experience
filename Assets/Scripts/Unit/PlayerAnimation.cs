using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [field: SerializeField]
    public Animator animator;
    
    public void EndLoad() {
        animator.SetInteger("LoadState",1);
    }

    public void StartLoad(){
        animator.SetInteger("LoadState",0);
    }

    public void StartUnload(){
        animator.SetInteger("LoadState",2);
    }
}

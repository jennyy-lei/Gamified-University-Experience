using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [field: SerializeField]
    public Animator animator;
    
    public void EndLoad() {
        animator.SetBool("loaded", true);
    }
}

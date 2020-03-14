﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Unit2,IJumpable,IShootable
{
    [field: SerializeField]
    public float jumpPow{get;set;}
    [field: SerializeField]
    public float shootDist{get;set;}
    [field: SerializeField]
    public GameObject bullet {get;set;}

    public void Update(){
        base.Update();
        
        restrainWithBg();
    }
    
    public void EndLoad() {
        animator.SetBool("loaded", true);
    }

    private void restrainWithBg(){
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp(pos.y, -1, 1);
        if(pos.y == 1){
            rb2d.velocity = Vector3.zero;
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }


}

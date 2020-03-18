﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullet : MonoBehaviour
{
    [SerializeField]
    private int value;
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Player2 player = other.gameObject.GetComponent<Player2>();
            player.incAmmo(value);
            
            Destroy(gameObject);
        }
    }
}

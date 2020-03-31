using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : Items
{   
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(StrConstant.playerTag)) {
            Player2 player = other.gameObject.GetComponent<Player2>();
            player.remainHealth += value;
            
            Destroy(gameObject);
        }
    }
}

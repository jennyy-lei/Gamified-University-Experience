using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player2 : Unit2,IJumpable,IShootable
{
    //Jumpable property
    [field: SerializeField]
    public float jumpPow{get;set;}

    //Shootable property
    [field: SerializeField]
    public float shootDist{get;set;}
    [field: SerializeField]
    public float bulletLimit{get;set;}
    [field: SerializeField]
    public Transform shootPos{get;set;}
    [field: SerializeField]
    public GameObject bullet {get;set;}
    public float bulletCount{get;set;}

    //text
    [field: SerializeField]
    public TextMeshProUGUI bulletText{get;set;}
    [field: SerializeField]
    public TextMeshProUGUI goldText{get;set;}

    //info
    private int gold = 0;
    public void Update(){
        base.Update();
        deadZone();
        restrainWithBg();
    }
    protected override void initSpawn(){
        spawnPoint = GameObject.Find("GameManager/PlayerSpawnPoint").transform;
        bulletCount = bulletLimit;
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

       public void deadZone() {
        // is below death point
        if (transform.position.y < -10) {
            rb2d.velocity = new Vector3(0, 0, 0);
            transform.position = spawnPoint.position;
            takeDmg(1);
            animator.SetBool("loaded", false);
        }
    }

    public void updateAmmo() {
        bulletText.text = bulletCount + " / " + bulletLimit;
    }

    public void updateGold() {
        goldText.text = gold.ToString();
    }

    public void incHealth(int amt) {
        remainHealth += amt;
        if (remainHealth > totalHealth) remainHealth = totalHealth;

        updateHealthBar();
    }

    public void incAmmo(int amt) {
        bulletCount += amt;
        if (bulletCount > bulletLimit) bulletCount = bulletLimit;

        updateAmmo();
    }

    public void incGold(int amt) {
        gold += amt;

        updateGold();
    }
}

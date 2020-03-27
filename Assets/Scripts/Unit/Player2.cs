using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player2 : Unit2,IJumpable,IShootable
{
    //Jumpable property
    [field: SerializeField]
    public float jumpPow{get;set;}
    [field: SerializeField]
    public int maxJumpNum{get;set;}
    [field: SerializeField]
    public int jumpNum{get;set;}
    public bool canJump
    {
        get => jumpNum < maxJumpNum;
    }

    //Shootable property
    [field: SerializeField]
    public float shootDist{get;set;}
    [field: SerializeField]
    public float bulletLimit{get;set;}
    [field: SerializeField]
    public Transform shootPos{get;set;}
    [field: SerializeField]
    public GameObject bullet {get;set;}

    private float _bulletCount;
    public float bulletCount
    {
        get => _bulletCount;
        set
        {
            _bulletCount = value;
            if (_bulletCount > bulletLimit) bulletCount = bulletLimit;
            updateBullet();

        }
    }

    //text
    [field: SerializeField]
    public TextMeshProUGUI bulletText{get;set;}
    [field: SerializeField]
    public TextMeshProUGUI goldText{get;set;}

    //info
    private int gold = 0;
    public void Update(){
        base.Update();
        int loadState = animator.GetInteger("LoadState");
        if(loadState == 2){
            animator.SetInteger("LoadState", 0);
        }
        else if(loadState == 1){
            restrainWithBg();
        }
        deadZone();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag(StrConstant.platformTag) && hitInfo.transform.position.y < transform.position.y) {
            jumpNum = 0;
        }
    }

    protected override void initSpawn(){
        spawnPoint = GameObject.Find(StrConstant.playerSpawnAddr).transform;
        bulletCount = bulletLimit;

        PlayerState state = LevelController.loadData<PlayerState>("PlayerState");

        if(state != null){
            this.remainHealth = state.remainHealth;
            this.gold = state.gold;
            this.facingRight = state.facingRight;
            this.bulletCount = state.bulletCount;
            this.transform.position = state.position.toVector2();
            if(!facingRight) transform.Rotate(0f,180f,0f);
        }
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
            remainHealth -= 1;
            animator.SetInteger("LoadState",2);
        }
    }

    public void updateBullet() {
        if (!bulletText) return;

        bulletText.text = bulletCount + " / " + bulletLimit;
    }

    public void updateGold() {
        if (!goldText) return;

        goldText.text = gold.ToString();
    }

    public void incGold(int amt) {
        gold += amt;

        updateGold();
    }

    public PlayerState getGameState(){
        PlayerState state = new PlayerState(transform.position);
        state.remainHealth = remainHealth;
        state.gold = gold;
        state.bulletCount = bulletCount;
        state.facingRight = facingRight;
        return state;
    }
}

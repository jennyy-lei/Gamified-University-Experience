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
        if(animator.GetBool("loaded")){
            restrainWithBg();
        }
        deadZone();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        string platformTag = "Platform";
        if (hitInfo.gameObject.CompareTag(platformTag) && hitInfo.transform.position.y < transform.position.y) {
            jumpNum = 0;
        }
    }

    protected override void initSpawn(){
        spawnPoint = GameObject.Find("GameManager/PlayerSpawnPoint").transform;
        bulletCount = bulletLimit;

        PlayerState state = LevelController.loadData<PlayerState>("PlayerState");

        if(state != null){
            this.remainHealth = state.remainHealth;
            this.gold = state.gold;
            this.facingRight = state.facingRight;
            this.bulletCount = state.bulletCount;
            this.transform.position = state.position.toVector2();
            Globals.setCharIndex(state.charIndex);
            if(!facingRight) transform.Rotate(0f,180f,0f);
        }

        changeChar();
    }

    private void changeChar() {
        GameObject newObj = (GameObject)Instantiate(Globals.getCharList()[Globals.getCharIndex()], transform.GetChild(0).position, transform.GetChild(0).rotation);
        newObj.transform.localScale = new Vector3(1, 1, 1);

        Destroy(transform.GetChild(0).gameObject);

        newObj.transform.SetParent(transform);
        newObj.transform.SetSiblingIndex(0);

        GetComponent<Player2>().animator = GetComponentInChildren<Animator>();
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
            animator.SetBool("loaded",false);
            animator.SetBool("unloaded", true);
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
        state.charIndex = Globals.getCharIndex();
        return state;
    }
}

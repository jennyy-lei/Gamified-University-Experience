using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Unit2 : MonoBehaviour
{

    public int id{get;private set;}
    [field: SerializeField]
    public int totalHealth{get;set;}
    public int remainHealth{get;set;}
    [field: SerializeField]
    public float atkDmg{get;set;}
    [field: SerializeField]
    public float atkRange{get;set;}
    [field: SerializeField]
    public float MAX_WALK_SPEED{get;set;}
    [field: SerializeField] 
    public float curSpeed {get; protected set;}
    public float walkSpeed{get;set;}


    [field: SerializeField] 
    public bool facingRight{get;set;}
    protected Animator animator {get;set;}
    protected Rigidbody2D rb2d {get;set;}
    protected Transform spawnPoint {get;set;}
    public bool isGrounded {get;set;}
    public bool isDead{get;set;}
    public bool isInit{get;set;}
    [field: SerializeField] 
    public Image healthBar{get;set;}

    protected Vector3 prevPos;
    public void Awake()
    {
        isGrounded = true;
        isDead = false;
        isInit = false;
        this.id = -1;
        walkSpeed = curSpeed = 0;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
        remainHealth = totalHealth;
        healthBar.fillAmount = getHealthRatio();
        initSpawn();
    }
    
    public float getHealthRatio()
    {
        return 1f * remainHealth/totalHealth;
    }

    public void takeDmg(int health)
    {
        remainHealth -= health;
        if (remainHealth < 0){
            remainHealth = 0;
            isDead = true;
        }

        healthBar.fillAmount = getHealthRatio();
    }

    public void Update(){
        updateCurSpeed();
        if(facingRight && curSpeed < -0.01) flip();
        if(!facingRight && curSpeed > 0.01) flip();
    }

    public void updateCurSpeed(){
        float diff = transform.position.x - prevPos.x;
        curSpeed = diff/Time.deltaTime;
        animator.SetFloat("speed", Mathf.Abs(curSpeed));
        prevPos = transform.position;
    }

    public void flip(){
        Debug.Log(curSpeed);
        transform.Rotate(0f,180f,0f);
        facingRight = !facingRight;
    }

    protected abstract void initSpawn();
}

public abstract class Enemy2 : Unit2
{
    [field: SerializeField]
    public float aggroRadius {get;set;}
}



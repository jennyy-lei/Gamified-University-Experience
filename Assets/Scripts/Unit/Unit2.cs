using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Unit2 : MonoBehaviour
{

    public int id{get;private set;}
    [field: SerializeField]
    public float totalHealth{get;set;}
    public float remainHealth{get;set;}
    [field: SerializeField]
    public float atkDmg{get;set;}
    [field: SerializeField]
    public float atkRange{get;set;}
    [field: SerializeField]
    public float MAX_WALK_SPEED{get;set;}
    [field: SerializeField] 
    public float walkSpeed{get;set;}


    [field: SerializeField] 
    public bool facingRight{get;set;}
    public Animator animator {get;set;}
    public Rigidbody2D rb2d {get;set;}
    public Transform spawnPoint {get;set;}
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
        walkSpeed = 0;
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
        remainHealth = totalHealth;

        updateHealthBar();
        initSpawn();
    }
    public void Update(){
    }

    private float getHealthRatio()
    {
        return remainHealth/totalHealth;
    }

    public void updateHealthBar()
    {
        healthBar.fillAmount = getHealthRatio();
    }

    public void takeDmg(float health)
    {
        remainHealth -= health;
        if (remainHealth <= 0){
            remainHealth = 0;
            isDead = true;
            ItemDrop dropController = GetComponent<ItemDrop>();
            if(dropController != null) dropController.Drop();
            destroy();
        }

        healthBar.fillAmount = getHealthRatio();
    }

    public void destroy()
    {
        if (remainHealth <= 0) {
            Destroy (gameObject);
        }
    }

    protected abstract void initSpawn();
}

public abstract class Enemy2 : Unit2
{
    [field: SerializeField]
    public float aggroRadius {get;set;}
    [field: SerializeField]
    public float knockbackForce {get;set;}
}



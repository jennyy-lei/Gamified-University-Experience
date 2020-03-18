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
        walkSpeed = 0;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
        remainHealth = totalHealth;

        updateHealthBar();
        initSpawn();
    }
    
    private float getHealthRatio()
    {
        return 1f * remainHealth/totalHealth;
    }

    public void updateHealthBar()
    {
        healthBar.fillAmount = getHealthRatio();
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
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Unit2 : MonoBehaviour
{
    [field: SerializeField]
    public float totalHealth{get;set;}
    private float _remainHealth;
    public float remainHealth
    {
        get => _remainHealth;
        set
        {
            _remainHealth = value;
            if (_remainHealth > totalHealth) _remainHealth = totalHealth;
            if (_remainHealth <= 0){
                _remainHealth = 0;
                ItemDrop dropController = GetComponent<ItemDrop>();
                if(dropController != null) dropController.Drop();
                Destroy(gameObject);
            }
            updateHealthBar();
        }
    }

    [field: SerializeField]
    public float MAX_WALK_SPEED{get;set;}
    [field: SerializeField] 
    public float moveSpeed{get;set;}
    public bool facingRight{get;set;}

    public Animator animator {get;set;}
    public Rigidbody2D rb2d {get;set;}
    public Transform spawnPoint {get;set;}
    [field: SerializeField] 
    public Image healthBar{get;set;}

    public bool isDead{
        get =>remainHealth <= 0;
    }

    public void Awake()
    {
        facingRight = true;
        moveSpeed = 0;
        animator = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        remainHealth = totalHealth;
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
        if (!healthBar) return;
        
        healthBar.fillAmount = getHealthRatio();
    }

    protected abstract void initSpawn();
}

public abstract class Enemy2 : Unit2
{
    [field: SerializeField]
    public float aggroRadius {get;set;}
}



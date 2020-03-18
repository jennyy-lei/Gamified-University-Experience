﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player2 info;

    private Animator playerAnim;
    private Rigidbody2D rb2d;

    private Command jumpCmd;
    private Command atkCmd;
    private Command moveCmd;

    private int jumpNum;
    [SerializeField]
    private Transform groundDetector;

    void Awake(){
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Player2>();
        rb2d = GetComponent<Rigidbody2D>();

        jumpNum = 0;

        jumpCmd = new JumpCmd(playerAnim,rb2d);
        atkCmd = new ShootCmd(playerAnim);
        moveCmd = new MoveCmd(playerAnim);
    }
    // Update is called once per frame
    void Update()
    {
        if(playerAnim.GetBool("loaded")){
            info.walkSpeed = Input.GetAxis("Horizontal") * info.MAX_WALK_SPEED;
            moveCmd.execute(transform, info);
            if (Input.GetButtonDown("Fire1")) {
                atkCmd.execute(transform,info);
                info.updateAmmo();
            }

            if (Input.GetButtonDown("Jump") && jumpNum <= 2) {
                jumpCmd.execute(transform,info);
                jumpNum++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 0.1f);
        string tagName = "Platform";
        Debug.Log(groundInfo.collider);
        if (hitInfo.gameObject.tag == tagName && groundInfo.collider) {
            // Debug.Log("collision!");
            jumpNum = 0;
        }
    }
}
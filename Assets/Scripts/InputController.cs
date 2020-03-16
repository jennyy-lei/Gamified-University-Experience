using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Player2 info;

    private Animator playerAnim;
    private Rigidbody2D rb2d;

    private Command jumpCmd;
    private Command atkCmd;
    private Command moveCmd;

    private int jumpNum;

    void Awake(){
        playerAnim = GetComponent<Animator>();
        info = GetComponent<Player2>();
        rb2d = GetComponent<Rigidbody2D>();

        jumpNum = 0;

        jumpCmd = new JumpCmd();
        atkCmd = new ShootCmd(info.bullet);
        moveCmd = new MoveCmd();
    }
    // Update is called once per frame
    void Update()
    {
        if(playerAnim.GetBool("loaded")){
            info.walkSpeed = Input.GetAxis("Horizontal") * info.MAX_WALK_SPEED;
            moveCmd.execute(player, info);
            if (Input.GetButtonDown("Fire1")) {
                atkCmd.execute(player,info);
                info.updateDisplay();
            }

            if (Input.GetButtonDown("Jump") && jumpNum <= 2) {
                jumpCmd.execute(player,info);
                jumpNum++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Platform") {
            jumpNum = 0;
        }
    }
}

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

    void Awake(){
        playerAnim = player.GetComponent<Animator>();
        info = player.GetComponent<Player2>();
        rb2d = player.GetComponent<Rigidbody2D>();

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
            if (Input.GetButtonDown("Fire1")) atkCmd.execute(player,info);
            if (Input.GetButtonDown("Jump")) jumpCmd.execute(player,info);
        }
        
    }
}

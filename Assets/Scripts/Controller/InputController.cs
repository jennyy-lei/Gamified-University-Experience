using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player2 info;

    private Command jumpCmd;
    private Command atkCmd;
    private Command moveCmd;

    private int jumpNum;

    [SerializeField]
    private Transform groundDetector;
    [SerializeField]
    private Weapon weapon;

    void Awake(){
        info = GetComponent<Player2>();

        jumpNum = 0;

        jumpCmd = new JumpCmd();
        atkCmd = new ShootCmd();
        moveCmd = new MoveCmd();
    }
    // Update is called once per frame
    void Update()
    {
        if(info.animator.GetBool("loaded")){
            info.walkSpeed = Input.GetAxis("Horizontal") * info.MAX_WALK_SPEED;
            moveCmd.execute(transform, info);

            if (Input.GetButtonDown("Fire1") && weapon.Attack()) {
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
        if (hitInfo.gameObject.CompareTag(tagName) && groundInfo.collider) {
            // Debug.Log("collision!");
            jumpNum = 0;
        }
    }
}
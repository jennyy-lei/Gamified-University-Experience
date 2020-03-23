using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player2 info;

    private Command jumpCmd;
    private Command atkCmd;
    private Command moveCmd;

    [SerializeField]
    private Transform groundDetector;
    private Weapon weapon;

    private bool allowInput;
    public float bounceCooldown;

    void Awake(){
        groundDetector = transform.GetChild(0).GetChild(0);
        weapon = GetComponentInChildren<Weapon>();

        info = GetComponent<Player2>();
        allowInput = true;
        jumpCmd = new JumpCmd();
        atkCmd = new ShootCmd();
        moveCmd = new MoveCmd();
    }
    // Update is called once per frame
    void Update()
    {
        if(info.animator.GetBool("loaded") && allowInput){
            info.moveSpeed = Input.GetAxis("Horizontal") * info.MAX_WALK_SPEED;
            moveCmd.execute(transform, info);

            if (Input.GetButtonDown("Fire1") && weapon.Attack()) {
                atkCmd.execute(transform,info);
            }

            if (Input.GetButtonDown("Jump") && ((IJumpable) info).canJump) {
                jumpCmd.execute(transform,info);
                ((IJumpable) info).jumpNum++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        string enemyTag = "Enemy";
        if(hitInfo.gameObject.CompareTag(enemyTag)){
            IMelee atkInfo = hitInfo.gameObject.GetComponent<IMelee>();
            info.remainHealth -= atkInfo.meleeDmg;
            Vector2 dir = hitInfo.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;
            info.rb2d.velocity = Vector2.zero;
            info.rb2d.AddForce(dir*atkInfo.knockbackForce,ForceMode2D.Impulse);
            allowInput = false;
            Invoke("resetCoolDown", bounceCooldown);
        }
    }

    private void resetCoolDown() => allowInput = true;
}
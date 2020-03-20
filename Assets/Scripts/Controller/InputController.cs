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

    private bool enabled;
    public float bounceCooldown;

    void Awake(){
        info = GetComponent<Player2>();
        jumpNum = 0;
        enabled = true;
        jumpCmd = new JumpCmd();
        atkCmd = new ShootCmd();
        moveCmd = new MoveCmd();
    }
    // Update is called once per frame
    void Update()
    {
        if(info.animator.GetBool("loaded") && enabled){
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
        string platformTag = "Platform";
        string enemyTag = "Enemy";
        
        if (hitInfo.gameObject.CompareTag(platformTag) && groundInfo.collider) {
            jumpNum = 0;
        }
        else if(hitInfo.gameObject.CompareTag(enemyTag)){
            Enemy2 enemyInfo = hitInfo.gameObject.GetComponent<Enemy2>();
            info.takeDmg(enemyInfo.atkDmg);
            Vector2 dir = hitInfo.GetContact(0).point - new Vector2(transform.position.x, transform.position.y);
            dir = -dir.normalized;
            info.rb2d.velocity = Vector2.zero;
            info.rb2d.AddForce(dir*enemyInfo.knockbackForce,ForceMode2D.Impulse);
            enabled = false;
            Invoke("resetCoolDown", bounceCooldown);
        }
    }

    private void resetCoolDown() => enabled = true;
}
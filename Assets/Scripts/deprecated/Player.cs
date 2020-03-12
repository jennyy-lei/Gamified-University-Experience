using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public float speed;
    public float jumpHeight;
    public Animator animator;
    public Transform spawnPoint;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Image healthBar;

    private Rigidbody2D rb2d;

    private PlayerMoveController moveController;

    public override void init()
    {
        rb2d = GetComponent<Rigidbody2D>();

        moveController = new PlayerMoveController(animator, rb2d, transform);

        transform.position = spawnPoint.position;
    }

    public override void attack() {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public override void deadZone() {
        // is below death point
        if (transform.position.y < -10) {
            transform.position = spawnPoint.position;
            rb2d.velocity = new Vector3(0, 0, 0);

            takeDmg(1);

            animator.SetBool("loaded", false);
        }
    }

    public override void moveSprite()
    {
        if (animator.GetBool("loaded")) {
            float moveHorizontal = Input.GetAxis("Horizontal") * speed;

            animator.SetFloat("speed", Mathf.Abs(moveHorizontal));

            moveController.move(moveHorizontal);

            if (Input.GetButtonDown("Jump")) {
                moveController.jump(jumpHeight);
            }
        }
        RestrainWithBg();
    }

    public override void updateHealthBar()
    {
        healthBar.fillAmount = getHealthRatio();
    }

    // function for animation events
    public void EndLoad() {
        animator.SetBool("loaded", true);
    }

    private void RestrainWithBg(){
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp(pos.y, -1, 1);
        if(pos.y == 1){
            rb2d.velocity = Vector3.zero;
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}

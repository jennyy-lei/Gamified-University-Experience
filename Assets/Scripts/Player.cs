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

    private MoveController moveController;

    public override void init()
    {
        rb2d = GetComponent<Rigidbody2D>();

        moveController = new MoveController(speed, jumpHeight, animator, rb2d, transform);

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

            takeDmg(1);

            animator.SetBool("loaded", false);
        }
    }

    public override void moveSprite()
    {
        if (animator.GetBool("loaded")) {
            moveController.updateMove();
        }
    }

    public override void updateHealthBar()
    {
        healthBar.fillAmount = getHealthRatio();
    }

    // function for animation events
    public void EndLoad() {
        animator.SetBool("loaded", true);
    }
}

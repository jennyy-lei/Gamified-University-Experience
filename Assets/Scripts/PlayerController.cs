using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Animator animator;
    public Transform spawnPoint;


    private Rigidbody2D rb2d;

    private UnitController unitController;
    private MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() ;

        unitController = new UnitController(10, 0, 0, 0);
        moveController = new MoveController(speed, jumpHeight, animator, rb2d, transform);

        transform.position = spawnPoint.position;
    }

    void Update()
    {
        // is below death point
        if (transform.position.y < -10) {
            transform.position = spawnPoint.position;

            animator.SetBool("loaded", false);
        }

        if (animator.GetBool("loaded")) {
            moveController.updateMove();
        }
    }

    public void EndLoad() {
        animator.SetBool("loaded", true);
    }
}

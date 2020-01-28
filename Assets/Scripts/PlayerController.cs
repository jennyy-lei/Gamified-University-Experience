using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Animator animator;

    private Rigidbody2D rb2d;

    private UnitController unitController;
    private MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() ;

        unitController = new UnitController(10, 0, 0, 0);
        moveController = new MoveController(speed, jumpHeight, animator, rb2d, transform);
    }

    void Update()
    {
        moveController.updateMove();
    }
}

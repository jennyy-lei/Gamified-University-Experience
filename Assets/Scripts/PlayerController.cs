using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public bool isGrounded = true;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>() ;
    }

    // Update is called once per frame
    void Update()
    {
        jump();
        move();
    }

    void move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        transform.position += movement * Time.deltaTime * speed;
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);

            // isGrounded = false;
        }
    }
}

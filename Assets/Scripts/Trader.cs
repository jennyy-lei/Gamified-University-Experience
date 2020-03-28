using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private GameObject textCanvas;
    
    [SerializeField]
    private GameObject nameText;
    [SerializeField]
    private GameObject convoText;

    private Vector2 textStartPosition;

    private bool isFacingRight;
    
    void Start()
    {
        textCanvas.SetActive(false);
        textStartPosition = textCanvas.transform.position;

        nameText.SetActive(true);
        convoText.SetActive(false);

        isFacingRight = true;
    }

    void Awake()
    {
        Debug.Log("hello?");
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.position);
        if(distance < 3) {
            textCanvas.SetActive(true);
            if (distance < 1) {
                nameText.SetActive(false);
                convoText.SetActive(true);

                Input();
            } else {
                nameText.SetActive(true);
                convoText.SetActive(false);
            }
        } else {
            textCanvas.SetActive(false);
        }
        
        textCanvas.transform.position = new Vector2(textStartPosition.x, textStartPosition.y + Mathf.Sin(Time.time * 3) * 0.05f);
    
        if (player.transform.position.x < transform.position.x && isFacingRight) {
            transform.GetComponent<SpriteRenderer>().flipX = true;;
            isFacingRight = false;
        } else if (player.transform.position.x > transform.position.x && !isFacingRight) {
            transform.GetComponent<SpriteRenderer>().flipX = false;;
            isFacingRight = true;
        }
    }

    private void Input()
    {

    }
}

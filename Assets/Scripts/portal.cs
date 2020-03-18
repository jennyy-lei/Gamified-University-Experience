using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject textCanvas;

    private Vector2 textStartPosition;
    
    void Start()
    {
        textCanvas.SetActive(false);

        textStartPosition = textCanvas.transform.position;
    }

    void Update()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.position);
        if(distance < 3) {
            textCanvas.SetActive(true);
        } else {
            textCanvas.SetActive(false);
        }

        textCanvas.transform.position = new Vector2(textStartPosition.x, textStartPosition.y + Mathf.Sin(Time.time * 3) * 0.05f);
    }
}

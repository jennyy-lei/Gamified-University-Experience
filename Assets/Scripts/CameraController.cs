using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float lerp_speed = 5f;

    public float topLimit;
    public float bottomLimit;
    public float rightLimit;
    public float leftLimit;


    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position  + offset, lerp_speed) + new Vector3(0, 0, -5);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,leftLimit,rightLimit),
            Mathf.Clamp(transform.position.y,bottomLimit,topLimit),
            transform.position.z
        );
        // transform.position = player.transform.position + offset;
    }
}

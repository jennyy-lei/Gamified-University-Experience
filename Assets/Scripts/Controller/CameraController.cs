using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform player;
    public float lerp_speed = 5f;
    public SpriteRenderer background;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.position - player.transform.position);
        offset = new Vector3(5.2f,2.7f,-5.0f);
        float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        float horzExtent = vertExtent * Screen.width / Screen.height;
        Bounds levelBounds = background.bounds;

        minX = levelBounds.min.x + horzExtent;
        maxX = levelBounds.max.x - horzExtent;
        minY = levelBounds.min.y + vertExtent;
        maxY = levelBounds.max.y - vertExtent;
        transform.position = new Vector3(
            Mathf.Clamp(player.position.x + offset.x,minX,maxX),
            Mathf.Clamp(player.position.y + offset.y,minY,maxY),
            transform.position.z
        );
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position  + offset, lerp_speed);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,minX,maxX),
            Mathf.Clamp(transform.position.y,minY,maxY),
            transform.position.z
        );
        //transform.position = player.transform.position + offset;
    }
}

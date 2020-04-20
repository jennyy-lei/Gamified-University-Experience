using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalType
{
    Null,
    Start,
    Teleport,
    Exit
}
public class Portal : MonoBehaviour
{
    public float stayDuration;
    public float tpTime;
    public PortalType type;

    private Transform player;

    private Player2 playerScript;

    [SerializeField]
    private GameObject textCanvas;

    private Vector2 textStartPosition;
    
    void Start()
    {
        textCanvas.SetActive(false);
        textStartPosition = textCanvas.transform.position;
        
    }

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerScript = player.GetComponent<Player2>();
        if(type == PortalType.Null){
            type = (PortalType) Random.Range((int)PortalType.Teleport,((int) PortalType.Exit) +1);
            Debug.Log(type);
        }

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

    void OnTriggerEnter2D(Collider2D hitInfo){
        if(hitInfo.gameObject.CompareTag("Player")){
            Invoke("teleport",stayDuration);
        }
    }

    void OnTriggerExit2D(Collider2D hitInfo){
        if(hitInfo.gameObject.CompareTag("Player")){
            CancelInvoke("teleport");
        }
    }

    void teleport(){
        player.GetComponentInChildren<PlayerAnimation>().StartUnload();
        Invoke("switchScene",tpTime);
    }
    void switchScene(){
        switch(type){
            case PortalType.Start:
                LevelController.startLevel();
                break;
            case PortalType.Exit:
                LevelController.exitLevel();
                break;
            case PortalType.Teleport:
                LevelController.switchLevel();
                break;
        }
    }
}

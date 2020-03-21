using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDetection : MonoBehaviour
{
    private EnemyController controller;
    private IDashable dashInfo;

    private bool isTriggered;

    public float aggroCD;

    void Awake(){
        controller = GetComponentInParent<EnemyController>();
        dashInfo = GetComponentInParent<IDashable>();

        isTriggered = false;
    }

    void OnTriggerStay2D(Collider2D c){
        if(isTriggered) return;
        isTriggered = true;
        Invoke("resetTrigger",aggroCD);
        dashInfo.dashTargetX = c.transform.position.x;
        controller.triggerAggro();
    }

    void resetTrigger() => isTriggered = false;
}

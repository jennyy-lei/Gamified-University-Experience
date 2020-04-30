using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectableEvent : MonoBehaviour,ISelectHandler
{
    public UnityEvent onSelect;
    void Start(){
        if(onSelect == null){
            onSelect = new UnityEvent();
        }
    }

    public void OnSelect (BaseEventData eventData) 
    {
        onSelect.Invoke();
    }
}

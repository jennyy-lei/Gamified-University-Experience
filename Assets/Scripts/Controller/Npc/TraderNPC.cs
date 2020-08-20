using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : Npc
{
    [SerializeField]
    private SelectScreen selectScreen;
    private GameObject[] selling;

    protected virtual void Awake(){
        List<GameObject> temp = new List<GameObject>(Globals.getItemList());
        temp.RemoveAt(1);
        selling = temp.ToArray();
        base.Awake();
    }

    override public void Open() {
        base.Open();
        selectScreen.initScreen(selling,selectEffect: (int i)=>{},buyEffect: (int i)=>{}, disableEffect: ()=>this.isActive=false);
        selectScreen.gameObject.SetActive(true);
    }

    override public void Close() {
        base.Close();
        selectScreen.gameObject.SetActive(false);
    }
}

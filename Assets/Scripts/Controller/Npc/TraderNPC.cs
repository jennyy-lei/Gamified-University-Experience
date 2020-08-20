using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : Npc
{
    [SerializeField]
    private SelectScreen selectScreen;
    private GameObject[] selling;

    protected virtual void Awake(){
        base.Awake();
        List<GameObject> temp = new List<GameObject>(Globals.getItemList());
        temp.RemoveAt(1);
        selling = temp.ToArray();
    }

    override public void Open() {
        base.Open();
        selectScreen.initScreen(selling,selectEffect: (int i)=>{},buyEffect: buyEffect,buyWarning: buyWarning);
        selectScreen.gameObject.SetActive(true);
    }
    void buyEffect(int index){
        switch(index){
            case 0: //Bullet
                ++selectScreen.playerScript.bulletCount;
                break;
            case 1: //Health
                ++selectScreen.playerScript.remainHealth;
                break;
        }
    }
    string buyWarning(int index){
        switch(index){
            case 0: //Bullet
                if(selectScreen.playerScript.bulletCount == selectScreen.playerScript.bulletLimit) return "Full";
                break;
            case 1:
                if(selectScreen.playerScript.remainHealth == selectScreen.playerScript.totalHealth) return "Full";
                break;
        }
        return "";
    }

    override public void Close() {
        base.Close();
        selectScreen.gameObject.SetActive(false);
    }
}

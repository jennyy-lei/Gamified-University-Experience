using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectNPC : Npc
{
    [SerializeField]
    private SelectScreen selectScreen;
    private int[] prices;
    private GameObject[] selling;

    protected virtual void Awake(){
        prices = new int[]{0,5};
        selling = Globals.getCharList();
        base.Awake();
    }

    override public void Open() {
        base.Open();
        selectScreen.initScreen(selling, prices, Globals.getCharIndex(),selectEffect: selectScreen.spriteScript.switchChar,buyEffect: buyEffect,buyWarning: buyWarning);
        selectScreen.gameObject.SetActive(true);
    }

    void buyEffect(int index){
        Globals.setCharIndex(index);
        this.Close();
    }
    string buyWarning(int index){
        if(Globals.getCharIndex() == index){
            return "Using";
        }
        return "";
    }

    override public void Close() {
        base.Close();
        selectScreen.gameObject.SetActive(false);
        if(Globals.getCharIndex() != selectScreen.getSelectedIndex()){
            selectScreen.spriteScript.switchChar(Globals.getCharIndex());
        }
    }
}

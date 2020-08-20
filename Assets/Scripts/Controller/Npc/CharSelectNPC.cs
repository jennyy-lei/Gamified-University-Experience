﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectNPC : Npc
{
    [SerializeField]
    private SelectScreen selectScreen;
    private GameObject[] selling;

    protected virtual void Awake(){
        selling = Globals.getCharList();
        base.Awake();
    }

    override public void Open() {
        base.Open();
        selectScreen.initScreen(selling,selectEffect: selectScreen.spriteScript.switchChar,buyEffect: buyEffect,Globals.getCharIndex());
        selectScreen.gameObject.SetActive(true);
    }

    void buyEffect(int index){
        Globals.setCharIndex(index);
        this.Close();
    }

    override public void Close() {
        base.Close();
        selectScreen.gameObject.SetActive(false);
        if(Globals.getCharIndex() != selectScreen.getSelectedIndex()){
            selectScreen.spriteScript.switchChar(Globals.getCharIndex());
        }
    }
}

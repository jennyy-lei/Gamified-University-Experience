using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterSelectScreen : SelectScreen
{
    protected virtual void Awake(){
        itemList = Globals.getCharList();
        selectedIndex = Globals.getCharIndex();
        base.Awake();
    }

    protected virtual void OnEnable(){
        selectedIndex = Globals.getCharIndex();
        base.OnEnabled();
    }

    public override void selectEffect(int index) {
        spriteScript.switchChar(index);
    }

    protected virtual void OnDisable(){
        if(Globals.getCharIndex() != selectedIndex){
            spriteScript.switchChar(Globals.getCharIndex());
        }
        base.OnDisable();
    }
}
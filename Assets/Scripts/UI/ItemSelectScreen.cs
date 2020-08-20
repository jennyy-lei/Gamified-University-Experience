using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSelectScreen : SelectScreen
{
    void Awake(){
        List<GameObject> temp = new List<GameObject>(Globals.getItemList());
        temp.RemoveAt(1);
        //itemList = temp.ToArray();
    }

    void OnEnable(){
        //selectedIndex = 0;
    }
}
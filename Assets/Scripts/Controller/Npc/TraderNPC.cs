using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderNPC : Npc
{
    [SerializeField]
    private GameObject selectScreen;

        override public void Open() {
        selectScreen.SetActive(true);
    }

    override public void Close() {
        selectScreen.SetActive(false);
    }
}

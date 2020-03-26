using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    private static readonly GameObject[] charList = Resources.LoadAll<GameObject>("Units/PlayerSprites/");

    public static GameObject[] getCharList () => charList;
}

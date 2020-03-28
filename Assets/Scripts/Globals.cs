using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    private static readonly GameObject[] charList = Resources.LoadAll<GameObject>("Units/PlayerSprites/");
    private static int charIndex = 0;

    // getters
    public static GameObject[] getCharList () => charList;
    public static int getCharIndex () => charIndex;
    
    // setters
    public static void setCharIndex (int index) {
        if (index >= 0 && index <= charList.Length)
            charIndex = index;
    }
}

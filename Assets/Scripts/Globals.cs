using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    private static readonly GameObject[] charList = Resources.LoadAll<GameObject>("Units/PlayerSprites/");
    private static int charIndex = 0;

    public static GameObject[] getCharList () => charList;
    public static void setCharIndex (int index) {
        if (index >= 0 && index <= charList.Length)
            charIndex = index;
    }
    public static int getCharIndex () => charIndex;
}

public static class StrConstant
{
    public const string playerSpawnAddr = "GameManager/PlayerSpawnPoint";
    public const string platformTag = "Platform";
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
    public const string bulletTag = "Bullet";
    

}

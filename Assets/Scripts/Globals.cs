using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    private static readonly GameObject[] charList = Resources.LoadAll<GameObject>("Units/PlayerSprites/");
    private static int charIndex = 0;

    private static readonly GameObject[] itemList = Resources.LoadAll<GameObject>("Items/");

    public static GameState gameState = GameState.get();
    public static PlayerState playerState = PlayerState.get();

    // getters
    public static GameObject[] getCharList () => charList;
    public static int getCharIndex () => charIndex;
    public static GameObject[] getItemList () => itemList;
    
    // setters
    public static void setCharIndex (int index) {
        if (index >= 0 && index <= charList.Length)
            charIndex = index;
    }

    // other functions
    public static float totalDropChance() {
        float max = 0;
        foreach (GameObject item in itemList) {
            //if(max <= item.GetComponent<IItem>().dropChance)
                max += item.GetComponent<IItem>().dropChance;
        }

        return max;
    }
}

public static class StrConstant
{
    public const string playerSpawnAddr = "GameManager/PlayerSpawnPoint";
    public const string platformTag = "Platform";
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
    public const string bulletTag = "Bullet";
}

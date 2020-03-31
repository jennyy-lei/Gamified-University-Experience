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

    private static GameState _gameState;
    public static GameState gameState{
        get{
            if(_gameState == null){
                _gameState = GameState.get();
            }
            return _gameState;
        }
        set{
            _gameState = value;
        }
    }
    public static PlayerState playerState = PlayerState.get();

    public static bool useStatePos = false;
}

public static class StrConstant
{
    public const string playerSpawnAddr = "GameManager/PlayerSpawnPoint";
    public const string platformTag = "Platform";
    public const string playerTag = "Player";
    public const string enemyTag = "Enemy";
    public const string bulletTag = "Bullet";
    

}

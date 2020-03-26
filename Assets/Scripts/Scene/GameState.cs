using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2State
{
    public float x;
    public float y;

    public Vector2State(Vector2 v){
        this.x = v.x;
        this.y = v.y;
    }
    
    public void fromVector2(Vector2 v){
        x = v.x;
        y = v.y;
    }

    public Vector2 toVector2(){
        return new Vector2(x,y);
    }

    public Vector3 toVector3(){
        return new Vector3(x,y,0);
    }

}

[System.Serializable]
public class PlayerState
{
    public float remainHealth;
    public float bulletCount;
    public bool facingRight;
    public int gold;
    public Vector2State position;

    public PlayerState(Vector2 position){
        remainHealth = bulletCount = 0f;
        gold = 0;
        this.facingRight = true;
        this.position = new Vector2State(position);
    }
}

public enum EnemyType
{
    Goose
}

[System.Serializable]
public class EnemyState
{
    public float remainHealth;
    public float facingRight;
    public Vector2State position;
    public EnemyType id;
}

[System.Serializable]
public class GameState
{
    public int curSceneIndex;
    public PlayerState playerState;
    public List<EnemyState> enemyState;

    public GameState(){
        curSceneIndex = 0;
    }
}

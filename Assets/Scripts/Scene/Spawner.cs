using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    void Awake(){
        Debug.Log("Spawner awake");
        enemy = Resources.Load<GameObject>("Units/Goose");
        generateEnemy();
    }

    public void generateEnemy(){
        Debug.Log("Generating enemy..." + enemy);
        Transform plats = GameObject.Find("Platforms").transform;
        if(plats == null){
            Debug.Log("Cannot generate enemy in this scene");
            return;
        }
        List<EnemyState> list = new List<EnemyState>();
        int count = plats.childCount;
        for(int i = 1; i < count; i++){
            Transform p = plats.GetChild(i);
            float xscale = p.localScale.x;
            int max = (int) (xscale + 0.5f);
            xscale *=1.5f;//space per unit
            for(float x = p.localScale.x; x > 0; x-=0.05f){
                if(max <= 0) break;
                if(Random.value > 0.90){
                    max--;
                    EnemyState es = new EnemyState();
                    es.remainHealth = 3;
                    es.facingRight = true;
                    es.id = EnemyType.Goose;
                    es.position = new Vector2State(p.position.x + Random.Range(-xscale,xscale),p.position.y + 0.51f);
                    es.facingRight = Random.Range(0,2) == 0;
                    list.Add(es);
                }
            }
        }
        spawnEnemy(list);
    }

    public void spawnEnemy(List<EnemyState> enemies){
        Transform parent = GameObject.Find("Enemies").transform;
        foreach(EnemyState e in enemies){
            Unit2 info = null;
            GameObject obj = null;
            switch(e.id){
                case EnemyType.Goose:
                    obj = GameObject.Instantiate(enemy,e.position.toVector3(),enemy.transform.rotation,parent);
                    info = obj.GetComponent<Unit2>();
                    break;
            }
            if(info != null){
                info.remainHealth = e.remainHealth;
                info.facingRight = e.facingRight;
                if(!info.facingRight) obj.transform.GetChild(0).Rotate(0f,180f,0f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int curSceneIndex;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        curSceneIndex = 0;
    }

    void Update(){
        if(test){
            test = false;
            switchScene(1);
        }
    } 

    public void switchScene(int index){
            SceneManager.LoadScene(index);
            curSceneIndex = index;
    }

}

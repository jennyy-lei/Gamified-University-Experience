using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController
{
    private static int curSceneIndex = 0;

    public static void switchScene(int index){
            SceneManager.LoadScene(index);
            curSceneIndex = index;
    }

}

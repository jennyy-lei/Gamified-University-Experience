using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
public class LevelController
{
    private static int curSceneIndex = 0;

    public static void switchScene(int index){
            SceneManager.LoadScene(index);
            curSceneIndex = index;
    }

    public static void save(){

    }

    public static void saveData<T>(T data, string fileName){
        string tempPath = Path.Combine(Application.persistentDataPath,"data");
        tempPath = Path.Combine(tempPath, fileName + ".txt");
        Debug.Log("Saved path: " + tempPath.Replace("/", "\\"));
        string jsonData = JsonUtility.ToJson(data,true);
        if(!Directory.Exists(Path.GetDirectoryName(tempPath))){
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
        }
        try{
            File.WriteAllText(tempPath,jsonData);
        }
        catch(Exception e){
            Debug.LogWarning("Failed to save data to: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }


    }

    public static T loadData<T>(string fileName){
        string tempPath = Path.Combine(Application.persistentDataPath,"data");
        tempPath = Path.Combine(tempPath, fileName + ".txt");

        if(!Directory.Exists(Path.GetDirectoryName(tempPath))){
            Debug.LogWarning("Directory does not exist");
            return default(T);
        }
        if(!File.Exists(tempPath)){
            Debug.LogWarning("File does not exist");
            return default(T);
        }
        string json = null;
        try{
            json = File.ReadAllText(tempPath);
            Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        return JsonUtility.FromJson<T>(json);
    }

}

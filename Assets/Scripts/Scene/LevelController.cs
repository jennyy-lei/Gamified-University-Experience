﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;

public class LevelController
{
    public static void switchScene(int index){
            SceneManager.LoadScene(index);
    }

    public static void save(){

    }

    public static void saveData<T>(T data, string fileName){
        string tempPath = Path.Combine(Application.persistentDataPath,"data");

        //Debug Purpose
        string jsonPath = Path.Combine(tempPath,fileName + "_readable.txt");
        string json = JsonUtility.ToJson(data,true);
        if(!Directory.Exists(Path.GetDirectoryName(jsonPath))){
            Directory.CreateDirectory(Path.GetDirectoryName(jsonPath));
        }
        try{
            File.WriteAllText(jsonPath,json);
        }
        catch(Exception e){
            Debug.LogWarning("Failed to save data to: " + jsonPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        //Real file
        tempPath = Path.Combine(tempPath, fileName + ".txt");
        Debug.Log("Saved path: " + tempPath.Replace("/", "\\"));
        string jsonData = JsonUtility.ToJson(data,false);
        byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);
        if(!Directory.Exists(Path.GetDirectoryName(tempPath))){
            Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
        }
        try{
            File.WriteAllText(tempPath,Convert.ToBase64String(jsonByte));
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

        return JsonUtility.FromJson<T>(Encoding.UTF8.GetString(Convert.FromBase64String(json)));
    }

}

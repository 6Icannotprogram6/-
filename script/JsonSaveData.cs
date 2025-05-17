using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonSaveData 
{
    private static JsonSaveData instance;
    public static JsonSaveData Instance()
    {
        if (instance == null)
        {
            instance = new JsonSaveData();
        }
        return instance;
    }
 

    public void SaveData()
    {
        Instruct ins = new Instruct();
      
        string jsonStr = JsonMapper.ToJson(ins);
        File.WriteAllText(Application.streamingAssetsPath + "/ScenceData.json", jsonStr);
        
    }

    public void LoadData()
    {
        string jsonStr = File.ReadAllText(Application.streamingAssetsPath + "/ScenceData.json");
        Instruct instructJson = JsonMapper.ToObject<Instruct>(jsonStr);
      
    }

}



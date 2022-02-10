using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class AchivementSerializedScript : MonoBehaviour
{
    [Serializable]
    public class AchivementClass
    {
        public String title;
        public String descr = null;
        public String date;
    }

    public AchivementClass achivement = new AchivementClass();
    
    private String fileName = "achivement";

    public void createFile()
    {
        // Init time
        achivement.date = achivementTime();
        
        String path =  $"{Application.dataPath}/{fileName}.txt";
        String json = JsonUtility.ToJson(achivement);
        
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"");
        }
        
        File.WriteAllText(path,json);
    }

    private String achivementTime()
    {
        DateTime localDate = DateTime.Today;
        return localDate.ToString("d");
    }
    
}

using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AchivementSerializedScript : MonoBehaviour
{
    [Serializable]
    public class AchivementClass
    {
        public String title;
        public String descr = null;
        public String date;
    }
    
    [Serializable]
    public class AchivementListClass
    {
        public AchivementClass[] achivement;
    }
    
    public AchivementListClass achivementList = new AchivementListClass();
    
    private String fileName = "achivement";

    public void createFile()
    {
        // Init time
        for (int i = 0; i < achivementList.achivement.Length; i++)
        {
            achivementList.achivement[i].date = achivementTime();
        }
        
        String path =  $"{Application.dataPath}/{fileName}.txt";
        String json = JsonUtility.ToJson(achivementList);
        
        if (!File.Exists(path))
        {
            File.WriteAllText(path,json);
        }
        else
        {
            File.AppendAllText(path,json);
        }
        
    }

    private String achivementTime()
    {
        DateTime localDate = DateTime.Today;
        return localDate.ToString("d");
    }
    

    [CanBeNull]
    public AchivementListClass readFile()
    {
        String path =  $"{Application.dataPath}/{fileName}.txt";
        if (File.Exists(path))
        {
            String content = File.ReadAllText(path);
            AchivementListClass tmp = new AchivementListClass();
            tmp = JsonUtility.FromJson<AchivementListClass>(content);
            return tmp;
        }
        else
        {
            return null;
        }
    }
    

}

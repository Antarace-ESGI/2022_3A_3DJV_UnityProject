using System;
using System.Collections;
using System.Collections.Generic;
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

    public void createFile()
    {
        String path =  Application.dataPath + "/achivement.txt";
        String json = JsonUtility.ToJson(achivement);
        if (!File.Exists(path))
        {
            File.WriteAllText(path,json);
        }
    }

}

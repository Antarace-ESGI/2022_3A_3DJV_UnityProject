using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementMenuScript : MonoBehaviour
{
    public Text txt;

    private void OnEnable()
    {

        AchivementSerializedScript script = new AchivementSerializedScript();
        List<AchivementSerializedScript.AchievementClass> achievements = new List<AchivementSerializedScript.AchievementClass>();
        achievements = script.readFile();

        if (achievements != null)
        {
            String content = "";
            for (int i = 0; i < achievements.Count; i++)
            {
                if (!string.IsNullOrEmpty(achievements[i].descr))
                {
                    content += $"{achievements[i].title}:\n{achievements[i].descr}\n{achievements[i].date}";
                }
                else
                {
                    content += $"{achievements[i].title}{achievements[i].date}";
                }

                content += "\n\n";
            }

            txt.text = content;
        }

    }
    
    
}

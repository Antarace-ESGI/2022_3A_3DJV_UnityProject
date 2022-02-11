using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementMenuScript : AchivementSerializedScript
{
    public Text txt;
    private void OnEnable()
    {
        AchivementListClass achivements = new AchivementListClass();
        achivements = readFile();

        if (achivements != null)
        {
            String content = "";
            for (int i = 0; i < achivements.achivement.Length; i++)
            {
                String title = achivements.achivement[i].title;
                String date = achivements.achivement[i].date;
                String descr = achivements.achivement[i].descr;
                if (!string.IsNullOrEmpty(descr))
                {
                    content += $"{title}:\n{descr}\n{date}";
                }
                else
                {
                    content += $"{title}    {date}";
                }

                content += "\n\n";
            }

            txt.text = content;
        }

    }
    
    
}

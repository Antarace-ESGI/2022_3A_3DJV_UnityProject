using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSingletonScript : MonoBehaviour
{
    // Public
    public static AchievementSingletonScript Instance { get; set; }
    public List<AchivementSerializedScript.AchievementClass> achievements;

    // Private
    
    private AchivementSerializedScript script = new AchivementSerializedScript();
    private Dictionary<int, bool> achievementManager = new Dictionary<int, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            achievements = new List<AchivementSerializedScript.AchievementClass>();
            achievements = script.readFile();
            initDictionary(achievements);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
    
    private void initDictionary(List<AchivementSerializedScript.AchievementClass> achievements)
    {
        // Init dictionary with default id : value
        
        const int nbAchievements = 6; // Fix the number of achievement of the game 

        for (int i = 0; i < nbAchievements; i++)
        {
            achievementManager.Add(i, false);
        }

        if (achievements != null && achievements.Count > 0)
        {
            foreach (AchivementSerializedScript.AchievementClass i in achievements)
            {
                achievementManager[i.id] = true;
            }
        }

    }

    private void Start()
    {
        // Test unlock first achievement : (Load the game for the first time)
        if (!achievementManager[0])
        {
            achievementManager[0] = true;
            script.achievement.init(0,"Première Partie", "Viens de lancer sa première partie");
            script.createFile();
        }
    }

    // Use for debug
    private void debug()
    {
        foreach (var achievement in achievementManager)
        {
            print(achievement.Value);
        }
    }
    
}

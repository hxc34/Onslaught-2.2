using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    Game Game;
    UI UI;
    public Dictionary<string, AchievementEntry> list = new Dictionary<string, AchievementEntry>();

    void Awake()
    {
        Game = Game.Get();
        UI = UI.Get();
        
        // Recursively parse achievements
        Read(transform);
        
    }

    // Recursively parse and find achievements
    public void Read(Transform obj)
    {
        foreach (Transform child in obj.transform) Read(child);
        AchievementEntry objBase = obj.GetComponent<AchievementEntry>();
        if (objBase != null) list.Add(objBase.id, objBase);
    }

    // Grant the achievement through normal means or steathily (like when loading)
    public bool GrantAchievement(string id)
    {
        // Achievement doesn't exist? Ignore
        if (!list.ContainsKey(id)) return false;

        AchievementEntry achievement = list[id];

        // User already has achievement? ignore
        if (list[id].unlocked) return false;

        // Display ui
        UI.NotificationUI.Display(new NotificationEntry(achievement.name, achievement.description, achievement.requireType, achievement.requireID));

        // Add to achievements list
        list[id].unlocked = true;

        return true;
    }

    public int GetProgress(string type, string id)
    {
        switch (type)
        {
            case "achievements":
                return list[id].unlocked == true ? 1 : 0;
            case "statistics":
                if (!Game.ProfileManager.activeProfile.statistics.ContainsKey(id)) return 0;
                return Game.ProfileManager.activeProfile.statistics[id];
            case "session-statistics":
                if (!Game.ProfileManager.activeProfile.sessionStatistics.ContainsKey(id)) return 0;
                return Game.ProfileManager.activeProfile.sessionStatistics[id];
        }

        return 0;
    }

    // Is the achievement unlocked?
    public bool IsUnlocked(string id)
    {
        return list[id].unlocked;
    }

    // Resets progression state for loading
    public void ResetState()
    {
        foreach (AchievementEntry tower in list.Values)
        {
            tower.unlocked = false;
        }
    }
}

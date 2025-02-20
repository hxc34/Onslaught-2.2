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

    void Start()
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

    public bool GrantAchievement(string id)
    {
        // No profile? Ignore
        if (Game.ProfileManager.activeProfile == null)
        {
            Debug.Log("[AchievementManager] There is no active profile active! Not checking achievements");
            return false;
        }

        // Check if achievement exists
        if (!list.ContainsKey(id))
        {
            Debug.Log($"[AchievementManager] There is no entry for achievement {id}");
            return false;
        }

        AchievementEntry achievement = list[id];

        // User already has achievement? ignore
        if (Game.ProfileManager.activeProfile.progression["achievements"].Contains(id)) return false;

        // Display ui
        UI.NotificationUI.Display(new NotificationEntry(achievement.name, achievement.description, achievement.iconX, achievement.iconY));

        // Add to achievements list
        Game.ProfileManager.activeProfile.progression["achievements"].Add(id);

        return true;
    }

    public int GetProgress(string type, string id)
    {
        // Sanity checks
        if (!Game.ProfileManager.IsProfileActive("AchievementManager")) return 0;
        if (!list.ContainsKey(id)) return 0;

        switch (type)
        {
            case "achievements":
                return Game.ProfileManager.activeProfile.progression["achievements"].Contains(id) ? 1 : 0;
            case "statistics":
                if (!Game.ProfileManager.activeProfile.statistics.ContainsKey(id)) return 0;
                return Game.ProfileManager.activeProfile.statistics[id];
            case "session-statistics":
                if (!Game.ProfileManager.activeProfile.sessionStatistics.ContainsKey(id)) return 0;
                return Game.ProfileManager.activeProfile.sessionStatistics[id];
        }

        return 0;
    }

    public bool IsUnlocked(string id)
    {
        if (!Game.ProfileManager.IsProfileActive("AchievementManager")) return false;
        return Game.ProfileManager.activeProfile.progression["achievements"].Contains(id);
    }
}

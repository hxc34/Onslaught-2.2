using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    Game Game;
    public AchievementNotification achievementNotification;
    public Dictionary<string, AchievementEntry> achievementEntries = new Dictionary<string, AchievementEntry>();

    void Start()
    {
        // Load achievements file (achievements.json) from resources
        Game = Game.Get();
        TextAsset jsonAchievements = Resources.Load<TextAsset>("achievements");
        var jsonDeserialized = JsonConvert.DeserializeObject<List<object>>(jsonAchievements.ToString());

        foreach (var entry in jsonDeserialized)
        {
            AchievementEntry newEntry = new AchievementEntry();
            Dictionary<string, object> ach = JsonConvert.DeserializeObject<Dictionary<string, object>>(entry.ToString());

            // brief check for id
            if (!ach.ContainsKey("id"))
            {
                Debug.Log("[AchievementManager] An achievement is missing an ID");
                continue;
            }

            if (achievementEntries.ContainsKey((string)ach["id"]))
            {
                Debug.Log($"[AchievementManager] An achievement with ID {(string)ach["id"]} already exists, ignoring");
                continue;
            }

            // generic
            newEntry.id = (string)ach["id"];
            newEntry.name = (string)ach["name"];
            newEntry.description = (string)ach["description"];

            // icon
            List<int> icon = JsonConvert.DeserializeObject<List<int>>(ach["icon"].ToString());
            newEntry.icon = (icon[0], icon[1]);

            // conditions
            if (ach.ContainsKey("conditions"))
            {
                List<object> conds = JsonConvert.DeserializeObject<List<object>>(ach["conditions"].ToString());
                foreach (object cond in conds)
                {
                    Dictionary<string, string> condEntry = JsonConvert.DeserializeObject<Dictionary<string, string>>(cond.ToString());

                    /* 
                     * Conditions are in the format:
                     * id: The ID of the condition, to search for statistic: "statistic:mystatistic", for achievement: "achievement:MyAchievementID"
                    */
                    AchievementCondition newConditition = new AchievementCondition();
                    if (condEntry.ContainsKey("title")) newConditition.title = condEntry["title"];
                    if (condEntry.ContainsKey("type")) newConditition.type = condEntry["type"];
                    if (condEntry.ContainsKey("id")) newConditition.id = condEntry["id"];
                    if (condEntry.ContainsKey("equality")) newConditition.equality = condEntry["equality"];
                    if (condEntry.ContainsKey("value"))  newConditition.value = float.Parse(condEntry["value"]);
                    newEntry.conditions.Add(newConditition);
                }
            }

            // rewards
            if (ach.ContainsKey("rewards"))
            {
                List<object> rewards = JsonConvert.DeserializeObject<List<object>>(ach["rewards"].ToString());
                foreach (object reward in rewards)
                {
                    Dictionary<string, string> rewEntry = JsonConvert.DeserializeObject<Dictionary<string, string>>(reward.ToString());
                    AchievementReward newReward = new AchievementReward();
                    newReward.type = int.Parse(rewEntry["type"]);
                    newReward.value = float.Parse(rewEntry["value"]);
                    newEntry.rewards.Add(newReward);
                }
            }

            // finally done setting, add it
            achievementEntries.Add(newEntry.id, newEntry);
        }
    }

    public bool Track(string name)
    {
        // No profile? Ignore
        if (Game.ProfileManager.activeProfile == null)
        {
            Debug.Log("[AchievementManager] There is no active profile manager active! Not checking achievements");
            return false;
        }

        // Achievement must exist, and meets conditions
        if (!achievementEntries.ContainsKey(name)) return false;

        // User already has achievement? ignore
        if (Game.ProfileManager.activeProfile.unlocks["achievements"].Contains(name)) return false;


        AchievementEntry entry = achievementEntries[name];
        if (!Check(entry)) return false;

        // Display ui
        achievementNotification.Display(entry);

        // Grant rewards to player
        // get granted rewards
        List<AchievementReward> grantedRewards = achievementEntries[name].rewards;
        // ...

        // Add to achievements list
        Game.ProfileManager.activeProfile.unlocks["achievements"].Add(name);

        return true;
    }

    // Verify statistics and unlocks against conditions, grant if valid
    public bool Check(AchievementEntry entry)
    {
        bool passed = true;
        foreach (AchievementCondition condition in entry.conditions)
        {
            bool result = true;

            // Lifetime statistics, session statistics, achievement unlocks, tower unlocks and spell unlocks
            switch (condition.type)
            {
                case "statistic":
                    result = CheckStatistic(condition, false);
                    break;
                case "statistic-session":
                    result = CheckStatistic(condition, true);
                    break;
                case "achievement":
                    result = CheckUnlock(condition, "achievements");
                    break;
                case "tower":
                    result = CheckUnlock(condition, "towers");
                    break;
                case "spell":
                    result = CheckUnlock(condition, "spells");
                    break;
            }

            // Not passed? Set to false
            if (!result) passed = false;
        }

        return passed;
    }

    // Check against statistics
    private bool CheckStatistic(AchievementCondition condition, bool session)
    {
        Dictionary<string, object> stats;

        // Check against session or lifetime statistics
        if (!session) stats = Game.Get().ProfileManager.activeProfile.statistics;
        else stats = Game.Get().ProfileManager.activeProfile.sessionStatistics;

        if (!stats.ContainsKey(condition.id))
        {
            Debug.Log($"[AchievementManager]: {condition.id} is not a valid ID in statistics, not granting");
            return false;
        }

        string cond = condition.equality;
        switch (cond)
        {
            case "=":
                if ((float)stats[condition.id] == condition.value) return true;
                break;
            case "!=":
                if ((float)stats[condition.id] != condition.value) return true;
                break;
            case ">":
                if ((float)stats[condition.id] > condition.value) return true;
                break;
            case "<":
                if ((float)stats[condition.id] < condition.value) return true;
                break;
            case "<=":
                if ((float)stats[condition.id] <= condition.value) return true;
                break;
            case ">=":
                if ((float)stats[condition.id] >= condition.value) return true;
                break;
            default:
                Debug.Log($"[AchievementManager]: {condition.equality} is not a valid equality in statistic condition {condition.id}, not granting");
                return false;
        }

        return false;
    }

    // Check against achievements, if it's in the list then it is unlocked
    private bool CheckUnlock(AchievementCondition condition, string type)
    {
        List<string> achievements = Game.Get().ProfileManager.activeProfile.unlocks[type];
        if (achievements.Contains(condition.id)) return true;
        return false;
    }

    public int GetProgress(AchievementCondition condition)
    {
        // Lifetime statistics, session statistics, achievement unlocks, tower unlocks and spell unlocks
        switch (condition.type)
        {
            case "statistic":
                Dictionary<string, object> stats = Game.ProfileManager.activeProfile.statistics;
                if (stats.ContainsKey(condition.id)) return (int)stats[condition.id];
                break;
            case "statistic-session":
                Dictionary<string, object> sessionStats = Game.ProfileManager.activeProfile.sessionStatistics;
                if (sessionStats.ContainsKey(condition.id)) return (int)sessionStats[condition.id];
                break;
            case "achievement":
                List<string> achUnlocks = Game.ProfileManager.activeProfile.unlocks["achievements"];
                return achUnlocks.Contains(condition.id) ? 1 : 0;
            case "tower":
                List<string> towerUnlocks = Game.ProfileManager.activeProfile.unlocks["towers"];
                return towerUnlocks.Contains(condition.id) ? 1 : 0;
            case "spell":
                List<string> spellUnlocks = Game.ProfileManager.activeProfile.unlocks["spells"];
                return spellUnlocks.Contains(condition.id) ? 1 : 0;
        }

        return 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEntry
{
    public string id = "AchievementEntry";
    public string name { get; set; } = "Empty Achievement";
    public string description { get; set; } = "I am an empty achievement!";
    public (int, int) icon { get; set; } = (0, 0);
    public List<AchievementCondition> conditions { get; set; } = new List<AchievementCondition>();
    public List<AchievementReward> rewards { get; set; } = new List<AchievementReward>();

    public bool Check(Dictionary<string, float> statistics)
    {
        bool passed = true;
        foreach (AchievementCondition condition in conditions) {
            bool condPassed = false;
            if (!statistics.ContainsKey(condition.id))
            {
                Debug.Log($"[AchievementEntry]: {condition.id} is not a valid ID in statistics, achievement {id}, not granting");
                passed = false;
                continue;
            }

            string cond = condition.equality;
            switch (cond)
            {
                case "=":
                    if (statistics[condition.id] == condition.value) condPassed = true;
                    break;
                case "!=":
                    if (statistics[condition.id] != condition.value) condPassed = true;
                    break;
                case ">":
                    if (statistics[condition.id] > condition.value) condPassed = true;
                    break;
                case "<":
                    if (statistics[condition.id] < condition.value) condPassed = true;
                    break;
                case "<=":
                    if (statistics[condition.id] <= condition.value) condPassed = true;
                    break;
                case ">=":
                    if (statistics[condition.id] >= condition.value) condPassed = true;
                    break;
                default:
                    Debug.Log($"[AchievementEntry]: {condition.equality} is not a valid equality in condition {condition.id}, achievement {id}, not granting");
                    passed = false;
                    break;
            }

            // condition not passed, don't grant reward
            if (!condPassed)
            {
                passed = false;
                break;
            }
        }

        return passed;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEntry
{
    public string id = "AchievementEntry";
    public string name = "Empty Achievement";
    public string description = "I am an empty achievement!";
    public (int, int) icon = (0, 0);
    public List<AchievementCondition> conditions = new List<AchievementCondition>();
    public List<AchievementReward> rewards = new List<AchievementReward>();
}

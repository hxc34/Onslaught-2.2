using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTest2 : AchievementEntry
{
    public AchievementTest2(string id, string name, string description, string icon) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
    }

    // Update is called once per frame
    public override bool Check(SessionStatistics sessionStatistics)
    {
        if (sessionStatistics.testValue > 0) return true;
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public SessionStatistics sessionStatistics;
    public AchievementNotification achievementNotification;
    private Dictionary<String, AchievementEntry> achievementEntries = new Dictionary<string, AchievementEntry>();

    // Start is called before the first frame update
    void Start()
    {
        // create and load achievements on startup
        achievementEntries.Add("AchievementTest", new AchievementTest("AchievementTest", "Test Achievement 1", "i am a test", "pom.png"));
        achievementEntries.Add("AchievementTest2", new AchievementTest2("AchievementTest2", "Test Achievement 2", "i'm also a test", "disembowel.png"));

        // load from local save
    }

    public void Track(string name)
    {
        // achievement must exist, and meets conditions
        if (!achievementEntries.ContainsKey(name)) return;
        if (!achievementEntries[name].Check(sessionStatistics)) return;

        // display ui
        achievementNotification.Display(achievementEntries[name]);
    }
}

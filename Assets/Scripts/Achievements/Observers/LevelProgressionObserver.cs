using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressionObserver : MonoBehaviour
{
    Game Game;
    public string achievementID;
    public int required = 1;

    void Start()
    {
        Game = Game.Get();
    }

    void Update()
    {
        // If the player already has reached the level, we can turn off this script
        if (!Game.ProfileManager.IsProfileActive()) return;
        if (Game.AchievementManager.IsUnlocked(achievementID))
        {
            gameObject.SetActive(false);
            return;
        }

        if (Game.ProfileManager.activeProfile.statistics["level"] >= required) Game.AchievementManager.GrantAchievement(achievementID);
    }
}

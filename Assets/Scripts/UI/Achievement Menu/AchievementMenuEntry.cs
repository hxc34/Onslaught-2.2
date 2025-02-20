using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Achievement menu entry for UI
public class AchievementMenuEntry : MonoBehaviour
{
    Game Game;
    public string achievementID = "MyAchievementID";
    public TMP_Text name, description, progress, rewardName, completed;
    public RectTransform icon, reward, progressBar;

    // Set info for achievement
    void Start()
    {
        Game = Game.Get();
    }

    public void Refresh()
    {
        AchievementEntry entry = Game.AchievementManager.list[achievementID];
        name.text = entry.name;
        description.text = entry.description;
        int amt = Game.AchievementManager.GetProgress(entry.requireType, entry.requireID);
        progress.text = $"{amt} / {entry.requireAmount}";
        icon.anchoredPosition = new Vector2(entry.iconX * -80, entry.iconY * -80);
        progressBar.sizeDelta = new Vector2(355 * (amt / entry.requireAmount), 41);
    }
}

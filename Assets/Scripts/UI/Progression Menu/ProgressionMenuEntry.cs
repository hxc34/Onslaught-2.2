using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProgressionMenuEntry : MonoBehaviour
{
    Game Game;
    public string requireType, requireID;
    public TMP_Text name, description, progress;
    public RectTransform progressBar;
    public RectTransform icon;

    void Start()
    {
        Game = Game.Get();
    }

    public void Refresh()
    {
        ProgressionEntry entry = null;
        if (requireType == "towers") entry = Game.ProgressionManager.towerList[requireID];
        else if (requireType == "spells") entry = Game.ProgressionManager.spellList[requireID];
        else return;

        name.text = entry.name;
        description.text = entry.description;
        if (Game.ProgressionManager.GetPlayerLevel() >= entry.requireLevel) progress.text = "Unlocked!";
        else progress.text = "Unlocked at Level " + entry.requireLevel;
        icon.anchoredPosition = new Vector2(entry.iconX * -80, entry.iconY * -80);
        progressBar.sizeDelta = new Vector2(571 * (Game.ProgressionManager.GetPlayerLevel() / entry.requireLevel), 41);
    }
}

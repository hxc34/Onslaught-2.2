using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSectionUI : MonoBehaviour
{
    // Container class for profile sections
    Game Game;
    public TMP_Text playerName, title, level;
    public RawImage icon;

    void Start()
    {
        Game = Game.Get();
    }

    public void Set(string title)
    {
        playerName.text = Game.ProfileManager.activeProfile.name;
        this.title.text = title;
        level.text = "Level " + Game.ProgressionManager.GetPlayerLevel();
    }
}

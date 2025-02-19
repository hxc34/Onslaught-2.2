using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSectionUI : MonoBehaviour
{
    // Container class for profile sections
    public TMP_Text playerName, title, level;
    public RawImage icon;

    public void Set(string name, string title, int level, int x, int y)
    {
        this.playerName.text = name;
        this.title.text = title;
        this.level.text = "Level " + level;
    }
}

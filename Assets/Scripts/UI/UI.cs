using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public NotificationUI NotificationUI;
    public AchievementMenuUI AchievementMenu;
    public ProgressionMenuUI ProgressionMenu;
    public SpellbarUI Spellbar;
    public CastbarUI Castbar;
    public StatsHoverUI StatsHover;

    public bool windowActive = false;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}

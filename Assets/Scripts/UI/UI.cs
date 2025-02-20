using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public NotificationUI NotificationUI;
    public AchievementMenuUI AchievementMenu;
    public ProgressionMenuUI ProgressionMenu;
    public ShopMenuUI ShopMenu;
    public GameplayToolbarUI GameplayToolbar;
    public SpellbarUI Spellbar;
    public CastbarUI Castbar;

    public bool windowActive = false;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}

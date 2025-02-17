using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public AchievementMenuUI AchievementMenu;
    public GameplayToolbarUI GameplayToolbar;
    public SpellbarUI Spellbar;
    public CastbarUI Castbar;

    public bool windowActive = false;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}

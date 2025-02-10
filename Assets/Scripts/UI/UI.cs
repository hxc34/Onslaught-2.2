using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public AchievementMenu AchievementMenu;
    public bool windowActive = false;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}

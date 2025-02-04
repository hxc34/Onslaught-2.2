using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEntry : MonoBehaviour
{
    public string id = "AchievementEntry";
    public string name = "Empty Achievement";
    public string description = "I am an empty achievement!";
    public int iconX = 0, iconY = 0;

    public virtual bool Check(SessionStatistics sessionStatistics)
    {
        Debug.Log("I am en empty achievement, edit me!");
        return false;
    }
}

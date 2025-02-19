using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCondition
{
    // title: what the condition is displayed as on the achievement menu
    public string title;

    // type: statistic, statistic-session, achievement, tower, spell
    public string type;

    // id: the id you want to compare against
    public string id;

    // equality (for statistics): < <= > >= == or !=
    public string equality;

    // value: the value
    public float value;
}

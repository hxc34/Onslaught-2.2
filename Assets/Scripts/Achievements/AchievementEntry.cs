using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEntry : MonoBehaviour
{
    public bool unlocked = false;
    public string id;
    public string name;
    public string description;
    public string requireType;
    public string requireID;
    public int requireAmount;
    public string rewardType;
    public string rewardID;
    public string rewardName;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionEntry : MonoBehaviour
{
    public bool unlocked = false;
    public string id;
    public string type;
    public string name;
    public string description;
    public int requireLevel;
    public List<string> cosmetics = new List<string>();
    public int iconX, iconY;
}

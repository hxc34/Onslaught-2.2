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

    public void ResetState()
    {
        unlocked = false;

        foreach (ProgressionUpgrade item in GetComponents<ProgressionUpgrade>())
        {
            item.level = 0;
        }
    }
}

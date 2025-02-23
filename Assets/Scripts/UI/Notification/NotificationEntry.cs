using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotificationEntry
{
    public string name, description, iconType, iconID;
    
    public NotificationEntry(string name, string description, string iconType, string iconID)
    {
        this.name = name;
        this.description = description;
        this.iconType = iconType;
        this.iconID = iconID;
    }
}

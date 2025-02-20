using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotificationEntry
{
    public string name, description;
    public (int, int) icon;
    
    public NotificationEntry(string name, string description, int x, int y)
    {
        this.name = name;
        this.description = description;
        this.icon = (x, y);
    }
}

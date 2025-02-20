using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileEntry
{
    public string name = "Player";
    public bool loaded = false;

    public Dictionary<string, int> statistics = new Dictionary<string, int>()
    {
        { "level", 0 },
        { "exp", 0 },
        { "unlockTokens", 0 },
        { "money", 999999 }
    };
    public Dictionary<string, int> sessionStatistics = new Dictionary<string, int>()
    {
        { "money", 0 }
    };
}

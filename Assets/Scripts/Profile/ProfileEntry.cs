using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileEntry
{
    public string name = "Player";

    public Dictionary<string, List<string>> progression = new Dictionary<string, List<string>>()
    {
        { "achievements", new List<string>() },
        { "towers", new List<string>() },
        { "spells", new List<string>() }
    };
    public Dictionary<string, Dictionary<string, string>> upgrades = new Dictionary<string, Dictionary<string, string>>()
    {
        { "towers", new Dictionary<string, string>() },
        { "spells", new Dictionary<string, string>() }
    };
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

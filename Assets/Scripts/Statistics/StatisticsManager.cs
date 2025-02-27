using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public Dictionary<string, int> statistics;
    public Dictionary<string, int> session;

    void Start()
    {
        ResetState();
    }

    // Reset statistics to the default state for profile loads
    public void ResetState()
    {
        statistics = new Dictionary<string, int>()
        {
            { "level", 0 },
            { "exp", 0 },
            { "unlockTokens", 0 },
            { "money", 999999 }
        };

        session = new Dictionary<string, int>()
        {
            { "money", 0 }
        };
    }
}

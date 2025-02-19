using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileEntry
{
    public string name = "Default Profile";
    
    // these can be simplified into a class...
    public Dictionary<string, List<string>> unlocks = new Dictionary<string, List<string>>();
    public Dictionary<string, Dictionary<string, string>> progression = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, object> statistics = new Dictionary<string, object>();
    public Dictionary<string, object> sessionStatistics = new Dictionary<string, object>();
}

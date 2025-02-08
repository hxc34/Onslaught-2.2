using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    Game Game;
    public SessionStatistics sessionStatistics;
    public AchievementNotification achievementNotification;
    public Dictionary<string, AchievementEntry> achievementEntries = new Dictionary<string, AchievementEntry>();

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        TextAsset jsonAchievements = Resources.Load<TextAsset>("achievements");
        var jsonDeserialized = JsonConvert.DeserializeObject<List<object>>(jsonAchievements.ToString());

        foreach (var entry in jsonDeserialized)
        {
            AchievementEntry newEntry = new AchievementEntry();
            Dictionary<string, object> ach = JsonConvert.DeserializeObject<Dictionary<string, object>>(entry.ToString());

            // brief check for id
            if (!ach.ContainsKey("id"))
            {
                Debug.Log("[AchievementManager] An achievement is missing an ID");
                continue;
            }

            if (achievementEntries.ContainsKey((string)ach["id"]))
            {
                Debug.Log($"[AchievementManager] An achievement with ID {(string)ach["id"]} already exists, ignoring");
                continue;
            }

            // generic
            newEntry.id = (string)ach["id"];
            newEntry.name = (string)ach["name"];
            newEntry.description = (string)ach["description"];

            // icon
            List<int> icon = JsonConvert.DeserializeObject<List<int>>(ach["icon"].ToString());
            newEntry.icon = (icon[0], icon[1]);

            // cconditions
            if (ach.ContainsKey("conditions"))
            {
                List<object> conds = JsonConvert.DeserializeObject<List<object>>(ach["conditions"].ToString());
                foreach (object cond in conds)
                {
                    Dictionary<string, object> condEntry = JsonConvert.DeserializeObject<Dictionary<string, object>>(cond.ToString());
                    AchievementCondition newConditition = new AchievementCondition();
                    newConditition.id = (string)condEntry["id"];
                    newConditition.equality = (string)condEntry["equality"];
                    newConditition.value = float.Parse((string)condEntry["value"]);
                    newEntry.conditions.Add(newConditition);
                }
            }

            // rewards
            if (ach.ContainsKey("rewards"))
            {
                List<object> rewards = JsonConvert.DeserializeObject<List<object>>(ach["rewards"].ToString());
                foreach (object reward in rewards)
                {
                    Dictionary<string, object> rewEntry = JsonConvert.DeserializeObject<Dictionary<string, object>>(reward.ToString());
                    AchievementReward newReward = new AchievementReward();
                    newReward.type = int.Parse((string)rewEntry["type"]);
                    newReward.value = float.Parse((string)rewEntry["value"]);
                    newEntry.rewards.Add(newReward);
                }
            }

            // finally done setting, add it
            achievementEntries.Add(newEntry.id, newEntry);
        }
    }

    public bool Track(string name)
    {
        // achievement must exist, and meets conditions
        if (!achievementEntries.ContainsKey(name)) return false;
        if (!achievementEntries[name].Check(Game.SessionStatistics)) return false;

        // display ui
        achievementNotification.Display(achievementEntries[name]);

        // grant rewards to player
        // get granted rewards
        List<AchievementReward> grantedRewards = achievementEntries[name].rewards;
        // ...

        return true;
    }
}

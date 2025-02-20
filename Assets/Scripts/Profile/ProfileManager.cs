using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Profiling;
using static UnityEngine.EventSystems.EventTrigger;

public class ProfileManager : MonoBehaviour
{
    Game Game;
    public ProfileEntry activeProfile;
    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
    }

    // Load a profile from My Documents/My Games/GhostTDGame
    public void Load()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\GhostTDGame";
        // Create "My Games" folder if it doesn't exist (standard folder for.. a lot of games)
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        // If the profile.json doesn't exist, make a new one and save it
        if (!File.Exists(path + "\\profile.json"))
        {
            Save();
            return;
        }

        // Otherwise, read from profile.json (temporary)
        StreamReader reader = new StreamReader(path + "\\profile.json");
        string content = "";
        string line = reader.ReadLine();
        try
        {
            while (line != null)
            {
                content += line;
                line = reader.ReadLine();
            }
        }
        catch (Exception e)
        {
            Debug.Log("[ProfileManager] Failed to read from profile.json! " + e);
        }
        reader.Close();

        try
        {
            // Create blank profile to read to
            activeProfile = new ProfileEntry();
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            ParseLoad(activeProfile, json);
            
        }
        catch (Exception e)
        {
            Debug.Log("[ProfileManager] Failed to parse from profile.json! " + e);
        }
    }

    private void ParseLoad(ProfileEntry profile, Dictionary<string, object> json)
    {
        // Clear everything that was there
        profile.statistics.Clear();
        profile.sessionStatistics.Clear();

        profile.name = (string)json["name"];

        // Parse progressions

        // Reset states...
        Game.AchievementManager.ResetState();
        Game.ProgressionManager.ResetState();

        // Starting with achievements
        var achievements = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["achievements"].ToString());
        foreach (Dictionary<string, object> item in achievements)
        {
            if ((string)item["unlocked"] == "1") Game.AchievementManager.list[(string)item["id"]].unlocked = true;
        }

        // Towers
        var towers = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["towers"].ToString());
        foreach (Dictionary<string, object> item in towers)
        {
            // Not a valid tower (for whatever reason)? Ignore
            if (!Game.ProgressionManager.IsValidEntry("towers", (string)item["id"])) continue;
            ProgressionEntry entry = Game.ProgressionManager.towerList[(string)item["id"]];

            var cosmetics = JsonConvert.DeserializeObject<List<string>>(item["cosmetics"].ToString());
            var upgrades = JsonConvert.DeserializeObject<List<string>>(item["upgrades"].ToString());
            entry.cosmetics.AddRange(cosmetics);
            entry.upgrades.AddRange(upgrades);
        }

        // Spells
        var spells = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json["spells"].ToString());
        foreach (Dictionary<string, object> item in spells)
        {
            // Not a valid spell (for whatever reason)? Ignore
            if (!Game.ProgressionManager.IsValidEntry("spells", (string)item["id"])) continue;
            ProgressionEntry entry = Game.ProgressionManager.towerList[(string)item["id"]];

            var cosmetics = JsonConvert.DeserializeObject<List<string>>(item["cosmetics"].ToString());
            var upgrades = JsonConvert.DeserializeObject<List<string>>(item["upgrades"].ToString());
            entry.cosmetics.AddRange(cosmetics);
            entry.upgrades.AddRange(upgrades);
        }

        // Parse statistics
        var statistics = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["statistics"].ToString());

        // We want them in int form.. the values are not hardcoded to allow for any values if one chooses (for modding)
        Dictionary<string, int> newStatistics = new Dictionary<string, int>();
        foreach (string stat in statistics.Keys)
        {
            newStatistics.Add(stat, int.Parse(statistics[stat]));
        }
        profile.statistics = newStatistics;

        activeProfile.loaded = true;
    }

    // Save current profile to a save file
    public void Save()
    {
        // No profile? Make one. It comes pre-inserted with relevant items
        if (activeProfile == null) activeProfile = new ProfileEntry();

        // Prepare save data
        Dictionary<string, object> save = new Dictionary<string, object>();
        List<object> achievements = new List<object>();
        List<object> towers = new List<object>();
        List<object> spells = new List<object>();

        save.Add("name", activeProfile.name);

        foreach (AchievementEntry entry in Game.AchievementManager.list.Values)
        {
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", entry.id);
            saveItem.Add("unlocked", entry.unlocked ? "1" : "0");
            achievements.Add(saveItem);
        }
        save.Add("achievements", achievements);

        foreach (ProgressionEntry entry in Game.ProgressionManager.towerList.Values)
        {
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", entry.id);
            saveItem.Add("cosmetics", entry.cosmetics);
            saveItem.Add("upgrades", entry.upgrades);
            towers.Add(saveItem);
        }
        save.Add("towers", towers);

        foreach (ProgressionEntry entry in Game.ProgressionManager.spellList.Values)
        {
            Dictionary<string, object> saveItem = new Dictionary<string, object>();
            saveItem.Add("id", entry.id);
            saveItem.Add("cosmetics", entry.cosmetics);
            saveItem.Add("upgrades", entry.upgrades);
            towers.Add(saveItem);
        }
        save.Add("spells", spells);

        // Statistics are stored as strings, but they are initially integers, convert them
        Dictionary<string, string> convertedStats = new Dictionary<string, string>();
        
        foreach (string stat in activeProfile.statistics.Keys)
        {
            convertedStats.Add(stat, activeProfile.statistics[stat].ToString());
        }
        save.Add("statistics", convertedStats);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\GhostTDGame";

        // Create "My Games" folder if it doesn't exist (standard folder for.. a lot of games)
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        StreamWriter writer = new StreamWriter(path + "\\profile.json");
        writer.Write(JsonConvert.SerializeObject(save));
        writer.Close();

        activeProfile.loaded = true;
    }

    public bool IsProfileActive(string s = "")
    {
        if (activeProfile == null || !activeProfile.loaded)
        {
            if (s != "") Debug.Log($"[ProfileManager] -> [{s}] There is no active profile!");
            return false;
        }
        return true;
    }
}

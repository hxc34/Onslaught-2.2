using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Profiling;

public class ProfileManager : MonoBehaviour
{
    Game Game;
    public ProfileEntry activeProfile;
    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();

        // test load
        Load();
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
        profile.progression.Clear();
        profile.upgrades.Clear();
        profile.statistics.Clear();
        profile.sessionStatistics.Clear();

        profile.name = (string)json["name"];

        // Parse progressions
        var progression = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["progression"].ToString());
        var achievements = JsonConvert.DeserializeObject<List<string>>(progression["achievements"].ToString());
        var towers = JsonConvert.DeserializeObject<List<string>>(progression["towers"].ToString());
        var spells = JsonConvert.DeserializeObject<List<string>>(progression["spells"].ToString());
        profile.progression.Add("achievements", achievements);
        profile.progression.Add("towers", towers);
        profile.progression.Add("spells", spells);

        // Parse upgrades
        var upgrades = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["upgrades"].ToString());
        var towersUp = JsonConvert.DeserializeObject<Dictionary<string, string>>(upgrades["towers"].ToString());
        var spellsUp = JsonConvert.DeserializeObject<Dictionary<string, string>>(upgrades["spells"].ToString());;
        profile.upgrades.Add("towers", towersUp);
        profile.upgrades.Add("spells", spellsUp);

        // Parse statistics
        var statistics = JsonConvert.DeserializeObject<Dictionary<string, string>>(json["statistics"].ToString());

        // We want them in int form.. the values are not hardcoded to allow for any values if one chooses (for modding)
        Dictionary<string, int> newStatistics = new Dictionary<string, int>();
        foreach (string stat in statistics.Keys)
        {
            newStatistics.Add(stat, int.Parse(statistics[stat]));
        }
        profile.statistics = newStatistics;
    }

    // Save current profile to a save file
    public void Save()
    {
        // No profile? Make one. It comes pre-inserted with relevant items
        if (activeProfile == null) activeProfile = new ProfileEntry();

        // Prepare save data
        Dictionary<string, object> save = new Dictionary<string, object>();
        save.Add("name", activeProfile.name);
        save.Add("progression", activeProfile.progression);
        save.Add("upgrades", activeProfile.upgrades);

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
    }

    public bool IsProfileActive(string source = "")
    {
        // No profile? Ignore
        if (activeProfile == null)
        {
            if (source != "") Debug.Log($"[{source}] There is no active profile!");
            return false;
        }

        return true;
    }
}

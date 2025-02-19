using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

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
            activeProfile.name = (string)json["name"];

            // Parse unlocks
            var unlocks = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["unlocks"].ToString());
            var achievements = JsonConvert.DeserializeObject<List<string>>(unlocks["achievements"].ToString());
            var towers = JsonConvert.DeserializeObject<List<string>>(unlocks["towers"].ToString());
            var spells = JsonConvert.DeserializeObject<List<string>>(unlocks["spells"].ToString());
            activeProfile.unlocks.Add("achievements", achievements);
            activeProfile.unlocks.Add("towers", towers);
            activeProfile.unlocks.Add("spells", spells);

            var progression = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["progression"].ToString());
            var achievementsprog = JsonConvert.DeserializeObject<Dictionary<string, string>>(progression["achievements"].ToString());
            var towersprog = JsonConvert.DeserializeObject<Dictionary<string, string>>(progression["towers"].ToString());
            var spellsprog = JsonConvert.DeserializeObject<Dictionary<string, string>>(progression["spells"].ToString());
            activeProfile.progression.Add("achievements", achievementsprog);
            activeProfile.progression.Add("towers", towersprog);
            activeProfile.progression.Add("spells", spellsprog);
        }
        catch (Exception e)
        {
            Debug.Log("[ProfileManager] Failed to parse from profile.json!" + e);
        }
    }

    // Save current profile to a save file
    public void Save()
    {
        // No profile? Make one
        if (activeProfile == null)
        {
            activeProfile = new ProfileEntry();

            activeProfile.unlocks.Add("achievements", new List<string>());
            activeProfile.unlocks.Add("towers", new List<string>());
            activeProfile.unlocks.Add("spells", new List<string>());
            activeProfile.progression.Add("achievements", new Dictionary<string, string>());
            activeProfile.progression.Add("towers", new Dictionary<string, string>());
            activeProfile.progression.Add("spells", new Dictionary<string, string>());
        }

        // Prepare save data
        Dictionary<string, object> save = new Dictionary<string, object>();
        save.Add("name", activeProfile.name);
        save.Add("unlocks", activeProfile.unlocks);
        save.Add("progression", activeProfile.progression);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\GhostTDGame";
        // Create "My Games" folder if it doesn't exist (standard folder for.. a lot of games)
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        StreamWriter writer = new StreamWriter(path + "\\profile.json");
        writer.Write(JsonConvert.SerializeObject(save));
        writer.Close();
    }
}

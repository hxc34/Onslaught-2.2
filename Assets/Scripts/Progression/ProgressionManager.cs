using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    Game Game;
    UI UI;

    public Dictionary<string, ProgressionEntry> towerList = new Dictionary<string, ProgressionEntry>();
    public Dictionary<string, ProgressionEntry> spellList = new Dictionary<string, ProgressionEntry>();

    void Awake()
    {
        Game = Game.Get();
        UI = UI.Get();

        // Recursively parse achievements
        Read(transform);
    }

    // Recursively parse and find achievements
    public void Read(Transform obj)
    {
        foreach (Transform child in obj.transform) Read(child);
        ProgressionEntry objBase = obj.GetComponent<ProgressionEntry>();
        if (objBase != null)
        {
            if (objBase.type == "towers") towerList.Add(objBase.id, objBase);
            if (objBase.type == "spells") spellList.Add(objBase.id, objBase);
        }
    }

    public bool IsValidEntry(string type, string id)
    {
        if (type == "towers") return towerList.ContainsKey(id);
        else if (type == "spells") return spellList.ContainsKey(id);
        else return false;
    }

    public int GetPlayerLevel()
    {
        if (!Game.ProfileManager.IsProfileActive()) return 0;
        return Game.ProfileManager.activeProfile.statistics["level"];
    }

    // Resets progression state for loading
    public void ResetState()
    {
        foreach (ProgressionEntry tower in towerList.Values)
        {
            tower.ResetState();
        }
    }
}

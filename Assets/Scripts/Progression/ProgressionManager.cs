using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    Game Game;
    UI UI;

    public GameObject towerContainer, spellContainer;

    public Dictionary<string, GameObject> towerList = new Dictionary<string, GameObject>();

    void Awake()
    {
        Game = Game.Get();
        UI = UI.Get();

        ReadTowers();
    }

    public void ReadTowers()
    {
        foreach (Transform child in towerContainer.transform)
        {
            ProgressionEntry objBase = child.GetComponent<ProgressionEntry>();
            if (objBase == null)
            {
                Debug.Log($"[ProgressionManager] A tower {child.name} is missing a ProgressionEntry!");
                continue;
            }

            towerList.Add(objBase.id, child.gameObject);
        }
    }

    public bool IsValidEntry(string type, string id)
    {
        if (type == "towers") return towerList.ContainsKey(id);
        else return false;
    }

    // Resets progression state for loading
    public void ResetState()
    {
        foreach (GameObject item in towerList.Values)
        {
            item.GetComponent<ProgressionEntry>().ResetState();
        }
    }
}

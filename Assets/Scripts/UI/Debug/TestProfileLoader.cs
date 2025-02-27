using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestProfileLoader : MonoBehaviour
{
    Game Game;
    public TMP_Text text, status;
    public Button loadButton, saveButton;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        loadButton.onClick.AddListener(Load);
        saveButton.onClick.AddListener(Save);
    }

    // Update is called once per frame
    void Load()
    {
        Game.ProfileManager.Load();
        text.text = $"Active profile:\n{Game.ProfileManager.playerName}";
        status.text = "loaded!";
    }

    void Save()
    {
        Game.ProfileManager.Save();
        status.text = "saved!";
    }
}

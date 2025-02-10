using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestProfileLoader : MonoBehaviour
{
    Game Game;
    public TMP_Text text;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Click()
    {
        ProfileEntry profile = Game.ProfileManager.activeProfile;
        if (profile == null) return;
        text.text = $"Active profile:\n{Game.ProfileManager.activeProfile.name}";
    }
}

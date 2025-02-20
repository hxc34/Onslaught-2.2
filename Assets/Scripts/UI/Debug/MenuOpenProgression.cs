using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpenProgression : MonoBehaviour
{
    Game Game;
    UI UI;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Open);
    }

    void Open()
    {
        ProfileEntry profile = Game.ProfileManager.activeProfile;
        UI.ProgressionMenu.profile.Set("Beginner Exterminator");
        UI.ProgressionMenu.Show();
    }
}

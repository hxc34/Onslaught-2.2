using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpenAchievements : MonoBehaviour
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
        UI.AchievementMenu.profile.Set(profile.name, "Beginner Exterminator", 0, 0, 0);
        UI.AchievementMenu.Show();
    }
}

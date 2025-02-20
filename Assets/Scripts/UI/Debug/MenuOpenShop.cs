using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOpenShop : MonoBehaviour
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
        UI.ShopMenu.profile.Set("Beginner Exterminator");
        UI.ShopMenu.Show();
    }
}

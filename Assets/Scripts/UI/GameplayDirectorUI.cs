using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDirectorUI : MonoBehaviour
{
    Game Game;
    UI UI;
    public GameObject gameplayHUD, directorHUD;
    bool directorActive = false;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
    }

    // Think of this as the Ghost director when you press tab in Destiny
    void Update()
    {
        if (UI.windowActive) return;
        
        directorActive = Input.GetKey(KeyCode.Tab);

        // if holding Tab, show the director hud (like a scoreboard)
        if (directorActive)
        {
            gameplayHUD.GetComponent<CanvasFade>().Hide();
            directorHUD.GetComponent<CanvasFade>().Show();
        }
        else
        {
            directorHUD.GetComponent<CanvasFade>().Hide();
            gameplayHUD.GetComponent<CanvasFade>().Show();
        }
    }
}

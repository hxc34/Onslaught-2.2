using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDirectorUI : MonoBehaviour
{
    UI UI;
    public CanvasVisible gameplayCanvas, directorCanvas;
    bool directorActive = false;

    void Start()
    {
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
            gameplayCanvas.Hide();
            directorCanvas.Show();
        }
        else
        {
            directorCanvas.Hide();
            gameplayCanvas.Show();
        }
    }
}

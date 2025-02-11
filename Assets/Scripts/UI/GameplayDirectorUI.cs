using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDirectorUI : MonoBehaviour
{
    UI UI;
    public GameObject gameplayHUD, directorHUD;
    bool directorActive = false;

    // Start is called before the first frame update
    void Start()
    {
        UI = UI.Get();
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.windowActive) return;
        
        directorActive = Input.GetKey(KeyCode.Tab);

        // if holding Tab, show the director hud (like a scoreboard)
        gameplayHUD.GetComponent<CanvasFade>().visible = !directorActive;
        gameplayHUD.GetComponent<CanvasGroup>().interactable = !directorActive;
        gameplayHUD.GetComponent<CanvasGroup>().blocksRaycasts = !directorActive;
        directorHUD.GetComponent<CanvasFade>().visible = directorActive;
        directorHUD.GetComponent<CanvasGroup>().interactable = directorActive;
        directorHUD.GetComponent<CanvasGroup>().blocksRaycasts = directorActive;
    }
}

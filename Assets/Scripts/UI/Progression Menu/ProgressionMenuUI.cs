using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ProgressionMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasFade canvas;
    public GameObject contentContainer;
    public Button closeButton;
    public ProfileSectionUI profile;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        closeButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        UI.windowActive = true;

        // Update everything
        foreach (Transform item in contentContainer.transform)
        {
            item.GetComponent<ProgressionMenuEntry>().Refresh();
        }

        canvas.Show();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}

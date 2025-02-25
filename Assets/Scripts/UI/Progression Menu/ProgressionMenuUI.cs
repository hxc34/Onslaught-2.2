using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasVisible canvas;
    public Button closeButton;
    public ProfileSectionUI profile;
    public ProgressionMenuListing listing;
    public ProgressionMenuInfo info;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        closeButton.onClick.AddListener(Hide);
    }

    public void ShowInformation(string type, string id)
    {
        listing.Hide();
        info.Show(type, id);
    }

    // Shows the main menu listing
    public void Show()
    {
        UI.windowActive = true;
        canvas.Show();
        listing.Show();
        info.Hide();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}

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
    CanvasFade canvas, listingCanvas, infoCanvas;
    public Button closeButton;
    public ProfileSectionUI profile;
    public ProgressionMenuListing listing;
    public ProgressionMenuInfo info;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        listingCanvas = listing.GetComponent<CanvasFade>();
        infoCanvas = info.GetComponent<CanvasFade>();
        closeButton.onClick.AddListener(Hide);
    }

    public void ShowInformation(string type, string id)
    {
        listingCanvas.Hide();
        infoCanvas.Show();
        info.Show(type, id);
    }

    public void Show()
    {
        UI.windowActive = true;
        listing.Show();
        canvas.Show();
        infoCanvas.Hide();
        listingCanvas.Show();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}

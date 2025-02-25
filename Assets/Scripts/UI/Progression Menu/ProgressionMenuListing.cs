using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuListing : MonoBehaviour
{
    CanvasVisible canvas, towerCanvas, spellCanvas;
    public RectTransform toggleArea;
    public GameObject towerContainer, spellContainer;
    public Button typeToggleButton;

    bool mode = false; // false = towers, true = spells

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        towerCanvas = towerContainer.GetComponent<CanvasVisible>();
        spellCanvas = spellContainer.GetComponent<CanvasVisible>();
        typeToggleButton.onClick.AddListener(SwitchType);
    }

    // Show and refresh the listing menu
    public void Show()
    {
        mode = false;
        canvas.Show();
        ShowTowers();
    }

    public void Hide()
    {
        canvas.Hide();
    }

    // Refresh all towers and spells container
    private void RefreshContainer(GameObject t)
    {
        foreach (Transform item in t.transform)
        {
            item.GetComponent<ProgressionMenuEntry>().Refresh();
        }
    }

    public void SwitchType()
    {
        mode = !mode;
        if (!mode) ShowTowers();
        else ShowSpells();
    }

    // Show list of towers
    private void ShowTowers()
    {
        toggleArea.anchoredPosition = new Vector2(0, 0);
        towerCanvas.Show();
        spellCanvas.Hide();
        RefreshContainer(towerContainer);
    }

    // Show list of spells
    private void ShowSpells()
    {
        toggleArea.anchoredPosition = new Vector2(365, 0);
        towerCanvas.Hide();
        spellCanvas.Show();
        RefreshContainer(spellContainer);
    }
}

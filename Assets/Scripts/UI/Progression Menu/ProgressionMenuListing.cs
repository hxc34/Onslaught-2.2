using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuListing : MonoBehaviour
{
    CanvasFade canvas, towerCanvas, spellCanvas;
    public RectTransform toggleArea;
    public GameObject towerContainer, spellContainer;
    public Button typeToggleButton;

    bool mode = false; // false = towers, true = spells

    void Start()
    {
        canvas = GetComponent<CanvasFade>();
        towerCanvas = towerContainer.GetComponent<CanvasFade>();
        spellCanvas = spellContainer.GetComponent<CanvasFade>();
        typeToggleButton.onClick.AddListener(SwitchType);
    }

    public void Show()
    {
        mode = false;
        ShowTowers();
    }

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

    private void ShowTowers()
    {
        toggleArea.anchoredPosition = new Vector2(0, 0);
        towerCanvas.Show();
        spellCanvas.Hide();
        RefreshContainer(towerContainer);
    }

    private void ShowSpells()
    {
        toggleArea.anchoredPosition = new Vector2(365, 0);
        towerCanvas.Hide();
        spellCanvas.Show();
        RefreshContainer(spellContainer);
    }
}

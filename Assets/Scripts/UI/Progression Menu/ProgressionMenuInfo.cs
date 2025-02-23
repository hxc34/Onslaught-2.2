using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuInfo : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasFade canvas;
    public TMP_Text title;
    public RawImage icon, banner;
    public Button back, upgradesButton, cosmeticsButton;
    public ProgressionMenuUpgrades upgrades;
    public ProgressionMenuCosmetics cosmetics;
    public CanvasFade menuCanvas;
    bool inMenu = false;
    ProgressionEntry currentEntry = null;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        back.onClick.AddListener(Back);
        upgradesButton.onClick.AddListener(ShowUpgrades);
        cosmeticsButton.onClick.AddListener(ShowCosmetics);
    }

    private void Back()
    {
        if (inMenu)
        {
            inMenu = false;
            menuCanvas.Show();
            upgrades.Hide();
            cosmetics.Hide();
        }
        else
        {
            UI.ProgressionMenu.Show();
            canvas.Hide();
        }
    }

    private void ShowUpgrades()
    {
        inMenu = true;
        menuCanvas.Hide();
        upgrades.Show(currentEntry);
    }

    private void ShowCosmetics()
    {
        inMenu = true;
        menuCanvas.Hide();
        cosmetics.Show();
    }

    public void Show(string type, string id)
    {
        GameObject item = null;

        if (type == "towers")
        {
            item = Game.ProgressionManager.towerList[id];
            cosmeticsButton.gameObject.SetActive(true);
        }
        else if (type == "spells")
        {
            item = Game.ProgressionManager.spellList[id];
            cosmeticsButton.gameObject.SetActive(false);
        }

        // No item? Don't do anything
        if (item == null) return;

        canvas.Show();
        menuCanvas.Show();
        upgrades.Hide();
        cosmetics.Hide();

        // set the stuff
        currentEntry = item.GetComponent<ProgressionEntry>();
        title.text = currentEntry.name;
        if (currentEntry.type != null && currentEntry.type != "") icon.texture = Resources.Load<Texture>($"Icons/{currentEntry.type}/{currentEntry.id}");

        
    }
}

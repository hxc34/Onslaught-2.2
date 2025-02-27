using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastbarUI : MonoBehaviour
{
    public TMP_Text castType, itemName;
    public RawImage icon;
    public Button cancelButton;

    Game Game;
    UI UI;
    CanvasVisible canvas;

    // Different cast types ie. "Casting", "Building"
    public enum Type { Cast, Build }

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasVisible>();
        cancelButton.onClick.AddListener(Hide);
    }

    // Set the castbar's text and cast type
    public void Set(GameObject entity, Type type)
    {
        ProgressionEntry prog = entity.GetComponent<ProgressionEntry>();
        itemName.text = prog.name;
        icon.texture = Resources.Load<Texture>($"Icons/{prog.type}/{prog.id}");
        if (type == Type.Cast) castType.text = "Casting";
        else if (type == Type.Build) castType.text = "Building";
    }

    public void Show()
    {
        UI.Spellbar.Hide();
        UI.BuildMenu.Hide();
        canvas.Show();
    }

    // Also re-enables the UI
    public void Hide()
    {
        Game.TowerPlacementManager.Stop();
        UI.Spellbar.Show();
        UI.BuildMenu.Show();
        canvas.Hide();
    }
}

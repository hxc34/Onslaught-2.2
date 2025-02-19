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
    public bool active = false;

    UI UI;
    CanvasFade canvas;

    // Different cast types ie. "Casting", "Building"
    public enum Type { Cast, Build }

    // Start is called before the first frame update
    void Start()
    {
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        cancelButton.onClick.AddListener(Hide);
    }

    // Update is called once per frame
    void Update()
    {
        // Right click cancels placement
        if (active && Input.GetMouseButtonUp(1)) Hide();
    }

    // Set the castbar's text and cast type
    public void SetText(string item, Type type, int itemX, int itemY)
    {
        itemName.text = item;
        if (type == Type.Cast) castType.text = "Casting";
        else if (type == Type.Build) castType.text = "Building";
    }

    public void Show()
    {
        UI.Spellbar.Disable();
        UI.GameplayToolbar.Disable();
        UI.windowActive = true;
        active = true;
        canvas.Show();
    }

    // Also re-enables the UI
    public void Hide()
    {
        UI.Spellbar.Enable();
        UI.GameplayToolbar.Enable();
        UI.windowActive = false;
        active = false;
        canvas.Hide();
    }
}

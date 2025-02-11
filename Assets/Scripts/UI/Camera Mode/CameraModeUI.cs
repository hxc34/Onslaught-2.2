using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraModeUI : MonoBehaviour
{
    Game Game;
    public Button openMenuBtn, overheadBtn, pivotBtn;
    public GameObject infoBox;
    public TMP_Text infoText;
    public float openRate = 5;
    RectTransform rect;
    bool tweening = false;
    bool open = false;

    void Start()
    {
        Game = Game.Get();
        rect = GetComponent<RectTransform>();
        openMenuBtn.onClick.AddListener(ToggleVisible);
        overheadBtn.onClick.AddListener(SetOverhead);
        pivotBtn.onClick.AddListener(SetPivot);
    }

    void Update()
    {
        if (tweening) {
            Vector3 pos = rect.anchoredPosition;
            if (open) {
                if (pos.x < 120) pos.x += openRate;
                else tweening = false;
            }
            else {
                if (pos.x > -120) pos.x -= openRate;
                else tweening = false;
            }

            rect.anchoredPosition = pos;
        }
    }

    private void ToggleVisible() {
        if (tweening) return;
        open = !open;
        tweening = true;

        if (open) infoBox.SetActive(true);
        else infoBox.SetActive(false);
    }

    private void SetOverhead() {
        Game.GameplayCameraController.SetOverhead(true);
        SetColours(overheadBtn, pivotBtn);
        infoText.text = "Top down camera to see the whole area.";
    }

    private void SetPivot() {
        Game.GameplayCameraController.SetOverhead(false);
        SetColours(pivotBtn, overheadBtn);
        infoText.text = "Pannable camera to get a closer view.";
    }

    private void SetColours(Button on, Button off) {
        ColorBlock onClrs = overheadBtn.colors;
        onClrs.normalColor = new Color(0, 187, 255);
        on.colors = onClrs;

        ColorBlock offClrs = pivotBtn.colors;
        offClrs.normalColor = new Color(100, 100, 100);
        off.colors = offClrs;
    }
}

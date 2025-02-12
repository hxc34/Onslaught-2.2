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
    public RectTransform openArrow;
    public TMP_Text infoText;
    public CanvasFade highlightFade;
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
        if (Input.GetKeyDown(KeyCode.V)) {
            if (Game.GameplayCameraController.overhead) SetPivot();
            else SetOverhead();
            highlightFade.GetComponent<CanvasGroup>().alpha = 1;
        }

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

        if (open) {
            infoBox.SetActive(true);
            openArrow.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else {
            infoBox.SetActive(false);
            openArrow.localRotation = Quaternion.Euler(0, 0, 180);
        }
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
        onClrs.normalColor = new Color(0/255f, 187f/255f, 255f/255f);
        on.colors = onClrs;

        ColorBlock offClrs = pivotBtn.colors;
        offClrs.normalColor = new Color(100f/255f, 100f/255f, 100f/255f);
        off.colors = offClrs;
    }
}

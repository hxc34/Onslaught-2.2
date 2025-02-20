using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ShopMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasFade canvas;
    public GameObject entryTemplate;
    public RectTransform canvasRect;
    public GameObject contentContainer;
    public Button closeButton;
    public ProfileSectionUI profile;
    public TMP_Text money;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        closeButton.onClick.AddListener(Hide);
    }

    void Update()
    {
        // Set money on UI
        if (Game.ProfileManager.activeProfile != null) money.text = Game.ProfileManager.activeProfile.statistics["money"].ToString();
    }

    public void Show()
    {
        UI.windowActive = true;
        canvas.Show();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayToolbarUI : MonoBehaviour
{
    public GameObject homeMenu, buildMenu;
    public Button buildButton;
    public float tweenRate = 5;

    CanvasFade canvas;
    RectTransform canvasRect;
    GameObject activeMenu;
    bool tweening = false;
    float tweenYGoal = 140;

    void Start()
    {
        activeMenu = homeMenu;
        buildButton.onClick.AddListener(OpenBuildMenu);
        canvas = GetComponent<CanvasFade>();
        canvasRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Tweening for the "scrolling" appear effect when a menu transitions
        if (tweening)
        {
            Vector3 s = canvasRect.anchoredPosition;
            bool moved = false;
            if (s.y < tweenYGoal)
            {
                s.y += tweenRate;
                moved = true;
            }

            if (!moved)
            {
                if (s.y > tweenYGoal)
                {
                    s.y -= tweenRate;
                    moved = true;
                }
            }

            if (!moved) tweening = false;

            canvasRect.anchoredPosition = s;
        }
    }

    private void OpenBuildMenu()
    {
        StartTween(180, buildMenu);
    }

    public void ReturnHomeMenu()
    {
        StartTween(85, homeMenu);
    }

    // Switch windows
    private void StartTween(float goal, GameObject newActive)
    {
        if (tweening) return;
        tweenYGoal = goal;
        tweening = true;
        activeMenu.GetComponent<CanvasFade>().Hide();
        newActive.GetComponent<CanvasFade>().Show();
        activeMenu = newActive;
    }

    public void Enable()
    {
        canvas.Show();
    }

    public void Disable()
    {
        canvas.Hide(0.05f);
    }
}

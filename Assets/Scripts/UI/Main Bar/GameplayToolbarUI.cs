using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayToolbarUI : MonoBehaviour
{
    public GameObject homeMenu, buildMenu, spellMenu;
    public Button buildButton, spellButton;
    public float tweenRate = 5;

    RectTransform canvas;
    GameObject activeMenu;
    bool tweening = false;
    float tweenYGoal = 140;

    // Start is called before the first frame update
    void Start()
    {
        activeMenu = homeMenu;
        buildButton.onClick.AddListener(OpenBuildMenu);
        spellButton.onClick.AddListener(OpenSpellMenu);
        canvas = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tweening)
        {
            Vector3 s = canvas.anchoredPosition;
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

            canvas.anchoredPosition = s;
        }
    }

    private void OpenBuildMenu()
    {
        StartTween(180, buildMenu);
    }

    private void OpenSpellMenu()
    {
        StartTween(180, spellMenu);
    }

    public void ReturnHomeMenu()
    {
        StartTween(85, homeMenu);
    }

    private void StartTween(float goal, GameObject newActive)
    {
        if (tweening) return;
        tweenYGoal = goal;
        tweening = true;
        activeMenu.GetComponent<CanvasFade>().visible = false;
        newActive.GetComponent<CanvasFade>().visible = true;
        activeMenu = newActive;
    }
}

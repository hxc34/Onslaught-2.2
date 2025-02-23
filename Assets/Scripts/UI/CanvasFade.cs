using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    public float rate = 5;

    float goal = 1;
    public bool visible = false;
    CanvasGroup panel;

    void Start() {
        panel = GetComponent<CanvasGroup>();

        // If UI is showing on screen (for design) but visible is not ticked, immediately hide it
        if (visible) Show();
        else Hide();
    }
    
    // Fade the UI depending on visible or not
    void Update()
    {
        if (visible)
        {
            if (panel.alpha < goal) panel.alpha += rate * Time.deltaTime;
        }
        else if (panel.alpha > goal)  panel.alpha -= rate * Time.deltaTime;
    }

    public void Show(float goal = 1)
    {
        visible = true;
        this.goal = goal;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    public void Hide(float goal = 0)
    {
        visible = false;
        this.goal = goal;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }
}

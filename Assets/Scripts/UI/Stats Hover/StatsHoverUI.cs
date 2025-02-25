using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatsHoverUI : MonoBehaviour
{
    public bool track = false;
    CanvasVisible canvas;
    RectTransform rect;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!track) return;
        rect.position = Input.mousePosition;
    }

    public void Show()
    {
        track = true;
        canvas.Show();
    }

    public void Hide()
    {
        track = false;
        canvas.Hide();
    }
}

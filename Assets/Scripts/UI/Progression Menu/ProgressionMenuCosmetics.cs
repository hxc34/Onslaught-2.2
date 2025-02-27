using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionMenuCosmetics : MonoBehaviour
{
    CanvasVisible canvas;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
    }

    public void Show()
    {
        canvas.Show();
    }

    public void Hide()
    {
        canvas.Hide();
    }
}

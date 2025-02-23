using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionMenuCosmetics : MonoBehaviour
{
    CanvasFade canvas;

    void Start()
    {
        canvas = GetComponent<CanvasFade>();
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

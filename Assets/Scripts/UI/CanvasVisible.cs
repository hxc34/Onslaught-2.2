using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisible : MonoBehaviour
{
    CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Hide(float amount = 0f)
    {
        canvas.alpha = amount;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}

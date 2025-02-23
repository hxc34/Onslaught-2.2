using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuUpgradeLevel : MonoBehaviour
{
    CanvasGroup canvas;
    RawImage image;
    public TMP_Text amount;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public void SetEquipped(bool active)
    {
        if (active)
        {
            image.color = new Color(0 / 255, 187 / 255, 255 / 255, 1);
        }
        else image.color = new Color(50 / 255, 50 / 255, 50 / 255, 1);
    }

    public void SetLocked(bool locked = true)
    {
        canvas.alpha = locked ? 0.3f : 1f;
        canvas.interactable = !locked;
        canvas.blocksRaycasts = !locked;
    }
}

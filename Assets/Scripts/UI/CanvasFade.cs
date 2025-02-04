using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    public bool visible = false;
    public int rate = 1;
    CanvasGroup panel;

    void Start() {
        panel = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (visible) {
            if (panel.alpha < 1) panel.alpha += rate * Time.deltaTime;
        }
        else if (panel.alpha > 0) panel.alpha -= rate * Time.deltaTime;
    }
}

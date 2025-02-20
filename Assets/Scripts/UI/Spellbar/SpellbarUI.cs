using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbarUI : MonoBehaviour
{
    public Button spell1, spell2, spell3;
    UI UI;

    CanvasFade canvas;

    void Start()
    {
        UI = UI.Get();
        spell1.onClick.AddListener(Spell1Click);
        spell2.onClick.AddListener(Spell2Click);
        spell3.onClick.AddListener(Spell3Click);
        canvas = GetComponent<CanvasFade>();
    }

    // Update is called once per frame
    void Update()
    {
        int selected = 0;
        // Pressing 1 2 or 3 will select the corresponding spell on the castbar
        if (Input.GetKey(KeyCode.Alpha1)) selected = 1;
        if (Input.GetKey(KeyCode.Alpha2)) selected = 2;
        if (Input.GetKey(KeyCode.Alpha3)) selected = 3;

        switch (selected) {
            case 1:
                Spell1Click();
                break;
            case 2:
                Spell2Click();
                break;
            case 3:
                Spell3Click();
                break;
        }
    }

    // Or clicking on them will also pull up the castbar
    private void Spell1Click()
    {
        UI.Castbar.SetText("Bufu", CastbarUI.Type.Cast, 0, 0);
        UI.Castbar.Show();
    }

    private void Spell2Click()
    {
        UI.Castbar.SetText("Zio", CastbarUI.Type.Cast, 0, 0);
        UI.Castbar.Show();
    }

    private void Spell3Click()
    {
        UI.Castbar.SetText("Agi", CastbarUI.Type.Cast, 0, 0);
        UI.Castbar.Show();
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

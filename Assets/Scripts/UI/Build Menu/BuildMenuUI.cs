using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuUI : MonoBehaviour
{
    // Temporarily hardcoded a tower... make it dynamic later
    // Start is called before the first frame update

    public Button build1;

    UI UI;

    void Start()
    {
        UI = UI.Get();
        build1.onClick.AddListener(Build1Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Build1Click()
    {
        UI.Castbar.SetText("Basic Tower", CastbarUI.Type.Build, 0, 0);
        UI.Castbar.Show();
    }
}

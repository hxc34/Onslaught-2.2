using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayToolbarReturn : MonoBehaviour
{
    UI UI;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        UI = UI.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Return);
    }

    // Update is called once per frame
    void Return()
    {
        UI.GameplayToolbar.ReturnHomeMenu();
    }
}

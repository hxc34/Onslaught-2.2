using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCameraToggle : MonoBehaviour
{
    Game Game;
    Button button;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Toggle);
    }

    void Toggle()
    {
        Game.GameplayCameraController.ToggleOverhead();
        if (Game.GameplayCameraController.overhead)
        {
            text.text = "Overhead Mode";
        }
        else text.text = "Pivot Mode: [WASD] Move, [RClick & Move Mouse Up/Down] Pan Up/Down, [LShift] Pan Faster";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCameraToggle : MonoBehaviour
{
    Game Game;
    Button button;
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
    }
}

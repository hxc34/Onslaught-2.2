using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestAchievement : MonoBehaviour
{
    
    Game Game;
    public TMP_Text text;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    public void Clicked() {
        if (!Game.AchievementManager.Track("DebugAchievement")) text.text = "already granted :(";
    }
}

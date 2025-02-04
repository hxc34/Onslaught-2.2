using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAchievement : MonoBehaviour
{
    
    Game Game;
    public SessionStatistics sessionStatistics;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    public void Clicked() {
        sessionStatistics.testValue = 1;
        Game.AchievementManager.Track("AchievementTest");
    }
}

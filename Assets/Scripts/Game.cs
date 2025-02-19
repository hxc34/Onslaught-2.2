using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public AchievementManager AchievementManager;
    public ShopManager ShopManager;
    public ProfileManager ProfileManager;
    public GameplayCameraController GameplayCameraController;

    public static Game Get() {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player main;

    public int health = 10;
    public int money = 250;
    public HealthUI healthUI;


    void Awake()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        health--;
        if (healthUI != null)
        {
            healthUI.RemoveLife();
        }
        Debug.Log("Player Lives: " + health);
    }
}

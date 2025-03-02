using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTower : Entity
{

    public GameObject spawner;

    void Start()
    {

        Debug.Log(speechBubblePrefab);
        if (speechBubblePrefab != null)
        {
            // Calculate the spawn position with the offset
            Vector3 spawnPosition = transform.position + speechBubbleOffset;
            // Instantiate the speech bubble at that position
            GameObject bubble = Instantiate(speechBubblePrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Bubble instantiated");
            // Optionally, if your SpeechBubble prefab has a script that allows setting text,
            // get that component and set the desired text.
            SpeechBubble bubbleScript = bubble.GetComponent<SpeechBubble>();
            if (bubbleScript != null)
            {
                //bubbleScript.SetText("Argh!"); // Or any text you wish
            }
        }
        spawner = GameObject.FindWithTag("Enemy Spawner");
        spawner.GetComponent<EnemySpawner>().FunnelEffect(1, 1.25f);
    }
}

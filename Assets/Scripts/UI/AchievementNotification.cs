using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNotification : MonoBehaviour
{
    public TMP_Text name, description;
    public RawImage icon;
    private AudioSource successSound;
    private Queue<AchievementEntry> displayQueue = new Queue<AchievementEntry>();
    private CanvasFade canvas;

    bool active = false;
    float time = 0;

    void Start() {
        canvas = GetComponent<CanvasFade>();
        successSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // show for a while
        if (active) {
            if (time > 5) canvas.visible = false;
            if (time > 5.5f) active = false;
            time += Time.deltaTime;
        }
        // queue not empty? dequeue and display
        else if (displayQueue.Count > 0) {
            // be sure to fade out before showing next achievement
            AchievementEntry entry = displayQueue.Dequeue();
            name.text = entry.name;
            description.text = entry.description;
            icon.rectTransform.anchoredPosition = new Vector2(entry.iconX * -80, entry.iconY * -80);
            active = true;
            time = 0;
            canvas.visible = true;
            successSound.Play();
        }
    }

    // add to queue
    public void Display(AchievementEntry entry) {
        if (!displayQueue.Contains(entry)) displayQueue.Enqueue(entry);
    }
}

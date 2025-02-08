using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchievementMenu : MonoBehaviour
{
    Game Game;
    CanvasFade canvas;
    public GameObject entryTemplate;
    public RectTransform canvasRect;
    public GameObject contentContainer;

    GameObject entryClone;

    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        canvas = GetComponent<CanvasFade>();
    }

    public void Refresh()
    {
        // clear list
        foreach (Transform obj in contentContainer.transform)
        {
            if (obj.name != "Entry Template") Destroy(obj.gameObject);
        }

        int gridX = 0;
        int gridY = 0;
        Dictionary<string, AchievementEntry> entries = Game.AchievementManager.achievementEntries;

        foreach (AchievementEntry entry in entries.Values)
        {
            GameObject tempClone = Instantiate(entryTemplate);
            tempClone.name = entry.id;
            tempClone.transform.parent = contentContainer.transform;
            tempClone.GetComponent<RectTransform>().GetComponent<RectTransform>().localPosition = new Vector2(gridX * 750, gridY * -170);
            tempClone.GetComponent<RectTransform>().GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            gridX++;
            if (gridX == 2)
            {
                gridX = 0;
                gridY++;
            }
            
            tempClone.SetActive(true);
        }
    }

    public void Show()
    {
        canvas.visible = true;
        Refresh();
    }

    public void Hide()
    {
        canvas.visible = false;
    }
}

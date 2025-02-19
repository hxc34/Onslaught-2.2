using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuUI : MonoBehaviour
{
    Game Game;
    UI UI;
    CanvasFade canvas;
    public GameObject entryTemplate;
    public RectTransform canvasRect;
    public GameObject contentContainer;
    public Button closeButton;
    public ProfileSectionUI profile;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        canvas = GetComponent<CanvasFade>();
        closeButton.onClick.AddListener(Hide);
    }

    public void Refresh()
    {
        // Clear the list first (implement filters later)
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
            tempClone.transform.SetParent(contentContainer.transform);

            // set entry data
            ShopMenuEntry data = tempClone.GetComponent<ShopMenuEntry>();
            data.name.text = entry.name;
            data.description.text = entry.description;
            //data.category.text = entry.category;
            data.icon.transform.Find("Icon").GetComponent<RectTransform>().anchoredPosition = new Vector2(entry.icon.Item1 * -80, entry.icon.Item2 * -80);
            
            // now show rewards
            // ...

            // set coordinates
            tempClone.GetComponent<RectTransform>().localPosition = new Vector2(gridX * 655, gridY * -215);
            tempClone.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

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
        UI.windowActive = true;
        canvas.Show();
        Refresh();
    }

    public void Hide()
    {
        UI.windowActive = false;
        canvas.Hide();
    }
}

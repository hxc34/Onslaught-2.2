using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionMenuUpgrades : MonoBehaviour
{
    CanvasVisible canvas;
    public ProgressionMenuUpgradeEntry upgrade1, upgrade2;

    void Start()
    {
        canvas = GetComponent<CanvasVisible>();
    }

    public void Show(GameObject entry)
    {
        canvas.Show();

        var items = entry.GetComponents<ProgressionUpgrade>();
        if (items.Length < 2) upgrade2.gameObject.SetActive(false);
        if (items.Length < 1) upgrade1.gameObject.SetActive(false);

        int i = 0;
        foreach (ProgressionUpgrade upgr in items)
        {
            if (i == 0)
            {
                upgrade1.name.text = upgr.name;
                upgrade1.level1.amount.text = upgr.amount1.ToString();
                upgrade1.level2.amount.text = upgr.amount2.ToString();
                upgrade1.level3.amount.text = upgr.amount3.ToString();
                upgrade1.SetLevel(upgr.level);
            }
            else if (i == 1)
            {
                upgrade2.name.text = upgr.name;
                upgrade2.level1.amount.text = upgr.amount1.ToString();
                upgrade2.level2.amount.text = upgr.amount2.ToString();
                upgrade2.level3.amount.text = upgr.amount3.ToString();
                upgrade2.SetLevel(upgr.level);
            }

            i++;
        }
    }

    public void Hide()
    {
        canvas.Hide();
    }
}

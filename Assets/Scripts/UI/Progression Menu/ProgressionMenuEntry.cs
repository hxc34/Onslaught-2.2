using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionMenuEntry : MonoBehaviour
{
    Game Game;
    UI UI;
    Button button;
    public string requireType, requireID;
    public TMP_Text name, description, progressText;
    public RectTransform progressBar;
    public RectTransform icon;

    void Start()
    {
        Game = Game.Get();
        UI = UI.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Tell the progression menu to display selected info
    private void Clicked()
    {
        UI.ProgressionMenu.ShowInformation(requireType, requireID);
    }

    // Refresh this entry's info
    public void Refresh()
    {
        // Not valid? Don't do anything
        ProgressionEntry entry = null;
        if (requireType == "towers") entry = Game.ProgressionManager.towerList[requireID].GetComponent<ProgressionEntry>();
        else if (requireType == "spells") entry = Game.ProgressionManager.spellList[requireID].GetComponent<ProgressionEntry>();
        else return;

        name.text = entry.name;
        description.text = entry.description;

        // Level met? Show "View Entry" instead
        if (Game.ProgressionManager.GetPlayerLevel() >= entry.requireLevel)
        {
            progressText.text = "View Entry";
        }

        // Set progress bar
        int level = Game.ProgressionManager.GetPlayerLevel();
        if (level >= entry.requireLevel) level = entry.requireLevel;
        progressBar.sizeDelta = new Vector2(418 * (level / entry.requireLevel), progressBar.sizeDelta.y);
    }
}

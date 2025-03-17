using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public Button playPauseButton;   // Assign in Inspector
    public Image buttonImage;        // The image on the button
    public Sprite playSprite;        // Sprite for "play"
    public Sprite pauseSprite;       // Sprite for "pause"
    public TMP_Text buttonText;      // Optional text label on the button
    public CanvasGroup uiCanvasGroup;  // The CanvasGroup component on your UI Canvas

    private bool gameStarted = false;
    private bool isPaused = false;

    public void OnPlayPauseButtonClicked()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1f;
            if (EnemyManager.main != null)
                EnemyManager.main.StartGame();

            // Switch to pause state.
            if (buttonImage != null)
                buttonImage.sprite = pauseSprite;
            if (buttonText != null)
                buttonText.text = "Pause";
            if (uiCanvasGroup != null)
            {
                uiCanvasGroup.interactable = true;
                uiCanvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            if (!isPaused)
            {
                // Pause the game.
                Time.timeScale = 0f;
                isPaused = true;
                if (buttonImage != null)
                    buttonImage.sprite = playSprite;
                if (buttonText != null)
                    buttonText.text = "Unpause";
                if (uiCanvasGroup != null)
                {
                    uiCanvasGroup.interactable = false;
                    uiCanvasGroup.blocksRaycasts = false;
                }
            }
            else
            {
                // Unpause the game.
                Time.timeScale = 1f;
                isPaused = false;
                if (buttonImage != null)
                    buttonImage.sprite = pauseSprite;
                if (buttonText != null)
                    buttonText.text = "Pause";
                if (uiCanvasGroup != null)
                {
                    uiCanvasGroup.interactable = true;
                    uiCanvasGroup.blocksRaycasts = true;
                }
            }
        }
    }
}

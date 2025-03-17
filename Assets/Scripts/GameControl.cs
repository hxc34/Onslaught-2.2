using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public Button playPauseButton;   // Assign via Inspector
    public Image buttonImage;        // The Image component on the button (or child)
    public Sprite playSprite;        // Sprite for "play"
    public Sprite pauseSprite;       // Sprite for "pause"

    private bool gameStarted = false;
    private bool isPaused = false;

    public TMP_Text buttonText;      // Optional: if you also want text to change

    public void OnPlayPauseButtonClicked()
    {
        if (!gameStarted)
        {
            // Start the game:
            gameStarted = true;
            Time.timeScale = 1f;
            if (EnemyManager.main != null)
            {
                EnemyManager.main.StartGame();
            }
            // Set the button image to pause (indicating you can pause the game now)
            Debug.Log("buttonImage: " + buttonImage);
            if (buttonImage != null)
                buttonImage.sprite = pauseSprite;
                
            if (buttonText != null)
                buttonText.text = "Pause";
        }
        else
        {
            // Toggle pause/unpause:
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;
                // When paused, change the button image to play (so user can click to unpause)
                if (buttonImage != null)
                    buttonImage.sprite = playSprite;
                if (buttonText != null)
                    buttonText.text = "Unpause";
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
                // When unpaused, change the button image back to pause
                if (buttonImage != null)
                    buttonImage.sprite = pauseSprite;
                if (buttonText != null)
                    buttonText.text = "Pause";
            }
        }
    }
}

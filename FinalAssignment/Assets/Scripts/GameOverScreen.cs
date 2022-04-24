using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    /* Text values for the game over UI */
    [SerializeField] private Text gameOverText; // the heading text
    [SerializeField] private Text pointsText; // player's score
    [SerializeField] private Text timeText; // remaining time in seconds
    [SerializeField] private Text totalScoreText; // total score after win

    public void Setup(int score, float time)
    {
        int seconds = Mathf.FloorToInt(time + 1); // time in seconds

        // When the game ends, make the UI active and display the player's score and remaining time
        gameObject.SetActive(true);
        pointsText.text = "Score: " + score + " points";
        timeText.text = "Time Remaining: " + seconds + " seconds";

        // If the player wins, display the seconds remaining as bonus points, and their total score
        if (GameManager.instance.isGameWon)
        {
            gameOverText.text = "YOU WIN!";
            timeText.text = "Time Bonus: " + seconds + " points";
            totalScoreText.text = "Final Score: " + (score + seconds) + " points";
        }
        // If the player doesn't win, final score is equal to player score
        else
        {
            gameOverText.text = "GAME OVER";
            totalScoreText.text = "Final Score: " + score + " points";
        }
    }

    // Restarts the Game scene
    public void RestartBtn()
    {
        SceneManager.LoadScene("GameScene");

        // If the player loses against the boss, stop the boss theme and play the level theme
        if (GameManager.instance.bossFightStarted)
        {
            SoundManager.instance.Stop("BossTheme");
            SoundManager.instance.Play("LevelTheme");
        }
        // If the player wins, restart the level theme from the start
        else if (GameManager.instance.isGameWon)
        {
            SoundManager.instance.Play("LevelTheme");
        }
    }

    // Loads the MainMenu scene
    public void ExitBtn()
    {
        SceneManager.LoadScene("MainMenu");

        // Stops all music if the player exits to the main menu
        SoundManager.instance.Stop("LevelTheme");
        SoundManager.instance.Stop("BossTheme");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;

    // Displays the score (based on the coins collected)
    private void DisplayScore()
    {
        scoreText.text = string.Format("Score: {0}", GameManager.instance.score);
    }

    // Displays the health of the player
    private void DisplayHealth()
    {
        healthText.text = string.Format("Health: {0}", GameManager.instance.health);
    }

    // Disables the player UI (score, health, timer) when the game ends
    private void DisableInterface()
    {
        if (GameManager.instance.isGameOver)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
        DisplayHealth();
        DisableInterface();
    }
}

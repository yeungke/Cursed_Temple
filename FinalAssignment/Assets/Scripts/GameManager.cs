using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Initialize single instance of GameManager
    public static GameManager instance;

    // Reference the game over screen
    public GameOverScreen gameOverScreen;

    // References the treasure (the win condition at the end of the game)
    public TreasureChest treasure;

    // Allows the player to quit by press this key
    [SerializeField] private KeyCode quitKey = KeyCode.Escape;

    /* Values that determine the current state of game */
    public float timeValue = 300f; // Timer for the game, default set to 5 minutes
    public int score; // Tracks the score of the game
    public int health = 3; // Tracks the player's health
    public bool canAttack; // Determine the player's ability to attack (false when climbing ladders)

    /* End conditions for the game */
    public bool isGameOver; // Ends the game, displays game over screen
    public bool bossFightStarted; // True if boss fight has started
    public bool isBossDead; // True if player has killed boss, spawn the treasure at the end of the level
    public bool isGameWon; // True if the player won the game (collected the treasure), displays win screen
    public bool hasQuit; // True if the player has quit the game (by pressing Escaoe)

    // Boolean values that determine the weapon the hero uses
    public bool isKnifeActive = true;
    public bool isAxeActive = false;
    public bool isCrossActive = false;

    void Start()
    {
        // Singleton pattern instance
        if (instance == null)
            instance = this;

        // Set all game end conditions to false
        isGameOver = false;
        bossFightStarted = false;
        isBossDead = false;
        isGameWon = false;
        hasQuit = false;

        // Set player health to 4
        health = 4;
    }

    // Determines which upgrade the player has: knife, axe or cross
    public void ActivateKnife()
    {
        isKnifeActive = true;
        isAxeActive = false;
        isCrossActive = false;
    }
    public void ActivateAxe()
    {
        isAxeActive = true;
        isCrossActive = false;
        isKnifeActive = false;
    }
    public void ActivateCross()
    {
        isCrossActive = true;
        isAxeActive = false;
        isKnifeActive = false;
    }

    // Counts the timer down; at 0, the game ends
    public void CountTime()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            GameEnd();
        }
    }

    // Ends the game, displays the default game over UI
    public void GameEnd()
    {
        isGameOver = true;
        gameOverScreen.Setup(score, timeValue);
    }

    // When the boss dies, activate the treasure object
    public void BossDies()
    {
        isBossDead = true;
        treasure.ActivateTreasure();
    }

    // End the game, displays the game's victory UI
    public void GameWin()
    {
        isGameWon = true;
        GameEnd();
    }

    // Allows player to end the game when they press the Quit Key
    public void QuitGame()
    {
        if (Input.GetKeyDown(quitKey) && !hasQuit)
        {
            // Displays the game over screen
            GameEnd();
            // Prevent the player from quitting more than once
            hasQuit = true;
            // Find and set the Player object to inactive
            FindObjectOfType<HeroLogic>().gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
        QuitGame();
    }
}

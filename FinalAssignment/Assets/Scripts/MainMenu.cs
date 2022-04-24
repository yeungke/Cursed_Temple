using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Calls the Game scene; plays the LevelTheme
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        SoundManager.instance.Play("LevelTheme");
    }

    // Closes the application
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has been closed");
    }
}

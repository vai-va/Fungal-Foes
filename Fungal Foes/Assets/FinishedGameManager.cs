using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedGameManager : MonoBehaviour
{
    public GameObject finishPanel;

    public void ShowFinishPanel()
    {
        // Activate the game over panel
        finishPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void NewGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Level1")
        {
            // Reload the current scene
            SceneManager.LoadScene("Level2");
            Time.timeScale = 1f;
        }
        if (currentScene == "Level2")
        {
            // Reload the current scene
            SceneManager.LoadScene("Level3");
            Time.timeScale = 1f;
        }
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("LevelMenu");
        Time.timeScale = 1f;
    }
}

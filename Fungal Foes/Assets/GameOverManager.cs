using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOverPanel()
    {
        // Activate the game over panel
        gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("LevelMenu");
        Time.timeScale = 1f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;

    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            // Save the completed level in PlayerPrefs
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level1");
        }
        else if (currentScene == "Level2")
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level2");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level3");
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigator1 : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenScene2()
    {
        // Check if Level1 is completed
        bool level1Completed = PlayerPrefs.HasKey("Level1") && PlayerPrefs.GetString("Level1") == "completed";

        // Only load Level2 if Level1 is completed
        if (level1Completed)
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            panel.SetActive(true);
            Debug.Log("Level 1 is not completed yet!");
            // Optionally, you can show a message or perform some other action here to indicate that Level 1 needs to be completed first.
        }
    }

    public void OpenScene3()
    {
        // Check if Level2 is completed
        bool level2Completed = PlayerPrefs.HasKey("Level2") && PlayerPrefs.GetString("Level2") == "completed";

        // Only load Level3 if Level2 is completed
        if (level2Completed)
        {
            SceneManager.LoadScene("Level3");
        }
        else
        {
            panel.SetActive(true);
            Debug.Log("Level 2 is not completed yet!");
            // Optionally, you can show a message or perform some other action here to indicate that Level 2 needs to be completed first.
        }
    }

    public void Resume()
    {
        panel.SetActive(false);
    }
}

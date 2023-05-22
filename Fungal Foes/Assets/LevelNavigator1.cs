using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigator1 : MonoBehaviour
{
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
        SceneManager.LoadScene("Level2");
    }

    public void OpenScene3()
    {
        SceneManager.LoadScene("Level3");
    }
}

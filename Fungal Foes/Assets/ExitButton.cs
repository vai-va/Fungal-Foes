using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}

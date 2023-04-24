using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI text;
    public int score;

    // Awake is called before the Start method
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (SceneManager.GetActiveScene().name == "Store" || SceneManager.GetActiveScene().name == "Level1")
        {
            if (PlayerPrefs.HasKey("CoinScore"))
            {
                score = PlayerPrefs.GetInt("CoinScore");
                text.text = "X" + score.ToString();
            }
        }
    }

    public int GetScore()
    {
        return score;
    }


    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        score += 1000;
        text.text = "X" + score.ToString();
        PlayerPrefs.SetInt("CoinScore", score);
        PlayerPrefs.Save();
    }
}


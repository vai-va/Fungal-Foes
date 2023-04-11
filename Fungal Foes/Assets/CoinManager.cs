using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI text;
    int score;

    // Start is called before the first frame update
    void Start()
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

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
        PlayerPrefs.SetInt("CoinScore", score);
        PlayerPrefs.Save();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnLocation;
    public float spawnInterval;
    private float spawntimer;
    public List<Enemy> enemies = new List<Enemy>();
    public float LevelValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public GameObject CoinPrefab;

    public float delay = 2f;
    public GameObject finishPanel;

    // Start is called before the first frame update
    void Start()
    {
        spawntimer = spawnInterval;
        GenerateWawe();
    }

    public void GenerateWawe()
    {
        GenerateEnemies();
    }
    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(LevelValue > 0)
        {
            int RandomEnemyId = Random.Range(0, enemies.Count);
            int RandomEnemycost = enemies[RandomEnemyId].cost;
            if(LevelValue-RandomEnemycost >= 0)
            {
                generatedEnemies.Add(enemies[RandomEnemyId].EnemyPrefab);
                LevelValue -= RandomEnemycost;
            }
            else if(LevelValue - RandomEnemycost <= 0)
            {
                // show game over panel after a delay
                //Invoke("ShowFinishPanel", delay);
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void ShowFinishPanel()
    {
        finishPanel.SetActive(true);
        Time.timeScale = 0f;

        // Get the current scene's name
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            // Save the completed level in PlayerPrefs
            PlayerPrefs.SetString(currentScene, "completed");
            PlayerPrefs.Save();
        }
        else if (currentScene == "Level2")
        {
            PlayerPrefs.SetString(currentScene, "completed");
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetString(currentScene, "completed");
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemiesToSpawn.Count > 0)
        {
            if (spawntimer <= 0)
            {
                Instantiate(enemiesToSpawn[0], SpawnLocation);
                enemiesToSpawn.RemoveAt(0);
                spawntimer = spawnInterval;
            }
            else
            {
                spawntimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            GameObject[] sceneObjects = FindObjectsOfType<GameObject>();
            bool flag = true;
            // Iterate through each object
            foreach (GameObject obj in sceneObjects)
            {
                if (obj.tag == "Enemy")
                {
                    flag = false;
                }
            }
            if (flag)
            {
                Invoke("ShowFinishPanel", delay);
            }
        }
    }

    public void DropCoins(Vector3 position, int random)
    {
        Coin coin = Instantiate(CoinPrefab, position, Quaternion.identity).GetComponent<Coin>();
        coin.coinValue = random;
    }

    [System.Serializable]
    public class Enemy
    {
        public GameObject EnemyPrefab;
        public int cost;

    }
}

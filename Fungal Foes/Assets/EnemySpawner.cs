using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform SpawnLocation;
    public float spawnInterval;
    private float spawntimer;
    public List<Enemy> enemies = new List<Enemy>();
    public float LevelValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public GameObject CoinPrefab;


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
            else if(LevelValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemiesToSpawn.Count > 0)
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

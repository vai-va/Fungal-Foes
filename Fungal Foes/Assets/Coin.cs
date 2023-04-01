using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue;

    public Coin(int value)
    {
        coinValue = value;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinManager.instance.ChangeScore(coinValue);

        }
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }

    }

}


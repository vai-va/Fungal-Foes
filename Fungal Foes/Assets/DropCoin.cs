using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    private Rigidbody2D item;
    public float dropForce = 5;

    private void Start()
    {
        item = GetComponent<Rigidbody2D>();
        item.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
    }
}

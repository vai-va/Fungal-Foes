using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreMenu", menuName = "Scriptable objects/New store item", order = 1)]
public class StoreItemSO : ScriptableObject
{
    public string title;
    public int baseCost;
    public Sprite image;
}

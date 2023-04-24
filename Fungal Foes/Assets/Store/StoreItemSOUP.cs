using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreMenu", menuName = "Scriptable objectsUP/New store item", order = 1)]
public class StoreItemSOUP : ScriptableObject
{
    public string title;
    public int baseCost;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Material

}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;

}

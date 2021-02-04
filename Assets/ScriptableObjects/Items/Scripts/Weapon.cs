using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Items/Weapon")]
public class Weapon : ItemObject
{

    public string wepName;
    public int level;
    public int baseDmg;
    public int blk;
    public int heal;
    public int range;
    public int speed;
    public string rarity;
    public Material material;
    public Color color;
    public Color textColor;
    [TextArea(5, 10)]
    public string special;
    public void Awake()
    {
        type = ItemType.Weapon;
    }
}

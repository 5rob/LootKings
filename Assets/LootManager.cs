using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{


    public List<Weapon> dropList;

    private int rarity;
    private int rarityMult;
    private string rarityName;
    private Material finalMaterial;
    private Color finalRarCol;
    private Color finalRarTextCol;

    public int[] rarityTable = {
        90, //Common
        50, //Uncommon
        25, //Rare
        10, //Super Rare
        5   //Legendary
        };

    public int[] rarityBoost = {
        5, //Common
        10, //Uncommon
        25, //Rare
        50, //Super Rare
        100   //Legendary
        };

    public string[] rarityNameTable ={
        "Common",
        "Uncommon",
        "Rare",
        "Super Rare",
        "legendary"
        };

    public Color[] rarityCol;

    public Color[] rarityTextCol;

    public Material[] rarityMatTable;

    private void Start()
    {
        //lootToDrop();
        //Debug.Log(rarityTable.Length);
    }


    Weapon wepToDrop;


    public Weapon LootToDrop()
    {
        int finalDmg = 0;
        int finalBlk = 0;
        int finalHeal = 0;
        int finalRange = 0;
        int finalSpeed = 0;
        string wepTypeName;



        //Reset();
        wepToDrop = ScriptableObject.CreateInstance<Weapon>();
        /*
        finalDmg = 0;
        finalBlk = 0;
        finalHeal = 0;
        finalRange = 0;
        finalSpeed = 0;
        finalMaterial = null;
        */

        //Select weapon type
        int randDrop = Random.Range(0, dropList.Count);
        wepToDrop = Instantiate(dropList[randDrop]);
        wepTypeName = wepToDrop.name.Replace("(Clone)","");


        //Get Rarity + mult
        GetRarity();
        rarityMult = GetRarityMultiplier(rarity);

        //wepToDrop.name = dropList[randDrop].ToString();
        

        //Randomize DMG
        if(wepToDrop.baseDmg != 0)
        {
            finalDmg = wepToDrop.baseDmg += Random.Range(rarityMult, rarityMult * 2);
        }        

        //Randomize BLK
        if(wepToDrop.blk != 0)
        {
            finalBlk = wepToDrop.blk += Random.Range(rarityMult, rarityMult * 2);
        }

        //Randomize HEAL
        if (wepToDrop.heal != 0)
        {
            finalHeal = wepToDrop.heal += Random.Range(rarityMult, rarityMult * 2);
        }

        //Randomize RANGE
        if (wepToDrop.range != 0)
        {
            finalRange = wepToDrop.range += Random.Range(rarityMult, rarityMult * 2);
        }

        //Randomize SPEED
        if (wepToDrop.speed != 0)
        {
            finalSpeed = wepToDrop.speed += Random.Range(rarityMult, rarityMult * 2);
        }

        wepToDrop.level = 1; // + random range based on quest progression <-------------------------- TODO
        wepToDrop.baseDmg = finalDmg;
        wepToDrop.blk = finalBlk;
        wepToDrop.heal = finalHeal;
        wepToDrop.range = finalRange;
        wepToDrop.speed = finalSpeed;
        wepToDrop.material = finalMaterial;
        wepToDrop.rarity = rarityName;
        wepToDrop.color = finalRarCol;
        wepToDrop.textColor = finalRarTextCol;
        wepToDrop.wepName = wepTypeName;






        string log = "Got a " + rarityName + " " + wepToDrop + ". With mult of " + rarityMult + ". That deals " + finalDmg + " damage.";
        Debug.Log(log);

        return wepToDrop;
    }


    //Get Rarity


    
    public void GetRarity()
    {
        int total = 0;
        int randomNum = 0;
        //reset
        rarity = 90;
        rarityName = "same name as last?";
        finalMaterial = rarityMatTable[0];

        foreach (var item in rarityTable)
        {
            total += item;
            
        }
        //Debug.Log(total);

        randomNum = Random.Range(0, total);


        //Debug.Log("randomNum = " + randomNum + ". total = " + total);

        for (int i = 0; i < rarityTable.Length; i++)
        {
            //Debug.Log("checking against " + rarityTable[i]);

            if (randomNum <= rarityTable[i])
            {
                //Debug.Log("found " + rarityTable[i]);
                //award item
                //Debug.Log("loot awarded");
                rarity = rarityTable[i];
                //Debug.Log(rarity);

                rarityName = rarityNameTable[i];
                //Debug.Log(rarityName);

                if (rarityName == rarityNameTable[i])
                {
                    //Debug.Log("rarity double up");
                }

                finalMaterial = rarityMatTable[i];
                //Debug.Log(finalMaterial);

                finalRarCol = rarityCol[i];
                finalRarTextCol = rarityTextCol[i];

                return;
            }
            else
            {
                randomNum -= rarityTable[i];
                
            }

        }

    }

    public int GetRarityMultiplier(int rareAmmount) 
    {
        int tmp = 0;

        for(int i = 0; rarityTable.Length > i; i++)
        {
            if(rarityTable[i] == rareAmmount)
            {
                tmp = rarityBoost[i];
            }
        }
        

        return tmp;
    }

    public void Reset()
    {

    }
}

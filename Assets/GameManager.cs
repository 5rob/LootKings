using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<Transform> enemyList;
    public int minEnemies;
    public int maxEnemies;
    public int kills;




    void Awake()
    {
        kills = 0;
        enemyList = new List<Transform>();
        

    }


}

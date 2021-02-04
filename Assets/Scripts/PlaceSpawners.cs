using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpawners : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject heroSpawner;
    public GameManager gameManager;
    public GetSpawnPos spawnPosData;
    public int numSpawns;
    public int numEnemies;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        numEnemies = gameManager.maxEnemies;
        numSpawns = spawnPosData.numSpawns;
        GameObject thisSpawner = new GameObject();
        Vector3 flip = new Vector3(0, 0, -180);
        Quaternion newQ = Quaternion.Euler(flip);

        Instantiate(heroSpawner, spawnPosData.spawnPozzys[numSpawns - 1], Quaternion.identity, thisSpawner.transform);



        for (int i = 0; (numSpawns -1) > i; i++)
        {
            
            Instantiate(enemySpawner, spawnPosData.spawnPozzys[i],Quaternion.identity, thisSpawner.transform);
            
        }

        thisSpawner.transform.localScale = transform.localScale;
        thisSpawner.transform.rotation = newQ;
    }
}

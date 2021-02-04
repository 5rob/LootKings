using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner_Static : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject enemyPrefab;
    public int numEnemySpawns;
    public List<Transform> enemyList;
    public List<Transform> myEnemys;
    public bool triggered;
    public GameObject enemySpawnerNull;







    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        numEnemySpawns = Random.Range(gameManager.minEnemies, gameManager.maxEnemies);
        enemyList = gameManager.enemyList;
        triggered = false;
        enemySpawnerNull = GameObject.FindGameObjectWithTag("EnemySpawnNull");






        for (int i = 0; numEnemySpawns > i; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-4f, 4f), 2, transform.position.z + Random.Range(-4f, 4f));

            Transform thisEnemy = Instantiate(enemyPrefab.transform, pos, Quaternion.identity, enemySpawnerNull.transform);

            gameManager.enemyList.Add(thisEnemy);
            myEnemys.Add(thisEnemy);
            EnemyLogic thisEnemyLogic = thisEnemy.GetComponent<EnemyLogic>();
            thisEnemyLogic.id = enemyList.Count + 1;
            thisEnemyLogic.health = Random.Range(1.0f, 100f);
            thisEnemyLogic.damage = Random.Range(1.0f, 3f);
            thisEnemyLogic.movementSpeed = Random.Range(2f, 7f);
            thisEnemyLogic.attackSpeed = Random.Range(1.0f, 3f);

        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Hero") && triggered == false)
        {
            triggered = true;
            for (int i = 0; myEnemys.Count > i; i++)
            {
                myEnemys[i].GetComponent<EnemyLogic>().agro = true;

            }
        }

    }




}

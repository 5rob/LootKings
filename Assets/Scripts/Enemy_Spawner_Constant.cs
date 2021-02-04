using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner_Constant : MonoBehaviour
{
    public Transform enemyPrefab;
    public GameManager gameManager;
    public List<Transform> enemyList;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        enemyList = gameManager.enemyList;
        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        for (; ; )
        {
            if (enemyList.Count <= gameManager.maxEnemies)
            {
                AddEnemy();
            }
            float rand = Random.value * 20f;
            yield return new WaitForSeconds(rand);
            //yield break;
        }

    }

    public void AddEnemy()
    {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(-4f, 4f), 2, transform.position.z);
        Transform thisEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        thisEnemy.name = "Enemy " + enemyList.Count.ToString();
        enemyList.Add(thisEnemy);
        EnemyLogic thisEnemyLogic = thisEnemy.GetComponent<EnemyLogic>();
        thisEnemyLogic.id = enemyList.Count + 1;
        thisEnemyLogic.health = Random.Range(1.0f, 100f);
        thisEnemyLogic.damage = Random.Range(1.0f, 3f);
        thisEnemyLogic.movementSpeed = Random.Range(2f, 7f);
        thisEnemyLogic.attackSpeed = Random.Range(1.0f, 3f);

    }
}

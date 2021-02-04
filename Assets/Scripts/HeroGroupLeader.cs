using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroGroupLeader : MonoBehaviour
{
    GameManager gameManager;
    public NavMeshAgent navAgent;
    Transform closeEnemy;
    CameraFollow cam;


    public bool isAttacking;
    float enemyDist;
    public float attackDist;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        navAgent = GetComponent<NavMeshAgent>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cam.enabled = true;

        //temps
        attackDist = 2f;
    }

    private void Update()
    {
        closeEnemy = FindEnemy();

        if (closeEnemy != null)
        {
            TrackEnemy();

            if (!isAttacking)
            {
                StartCoroutine(MoveToEnemy());
            }
            if (enemyDist <= attackDist && isAttacking == false)
            {
                EventManager.current.AttackBegin();
                isAttacking = true;
            }
            if (enemyDist >= attackDist)
            {
                EventManager.current.AttackEnd();
                isAttacking = false;
            }


        }
    }

    public Transform FindEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        if (gameManager.enemyList.Count > 0)
        {
            foreach (Transform enemyTransform in gameManager.enemyList)
            {
                float dist = Vector3.Distance(enemyTransform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = enemyTransform;
                    minDist = dist;
                }
            }
            return tMin;
        }
        else
        {
            return null;
        }

    }

    public void TrackEnemy()
    {
        enemyDist = Vector3.Distance(closeEnemy.position, transform.position);

    }

    IEnumerator MoveToEnemy()
    {
        navAgent.SetDestination(closeEnemy.position);
        yield return new WaitForSeconds(0.5f);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HeroLogic : MonoBehaviour
{
    GameManager gameManager;
    Transform closeEnemy;
    public NavMeshAgent navAgent;
    Transform heroGroupLeader;

    float enemyDist;
    public float attackDist;
    public float moveSpeed;
    public float turnSpeed;
    public float attackDamage;
    public float attackSpeed;
    public bool isAttacking;

    private Animator anim;

    private float fastAnim;
    private float smoothAnim;

    private bool canAttack;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        EventManager.current.OnAttackBegin += AttackBegin;
        EventManager.current.OnAttackEnd += AttackEnd;
        heroGroupLeader = GameObject.FindGameObjectWithTag("HeroLeader").transform;

        isAttacking = false;
        canAttack = false;
        fastAnim = 0.0f;
    }


    void Update()
    {
        float delta = fastAnim - smoothAnim;
        delta *= (Time.deltaTime * 100.0f);

        smoothAnim += delta;

        anim.SetFloat("Speed", fastAnim);

        if (canAttack)
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
                    StartCoroutine(AttackAndWait());
                }
                if (enemyDist >= attackDist)
                {
                    StopCoroutine(AttackAndWait());
                }
            }
            else
            {
                fastAnim = 0.0f;
            }


        }
        else
        {
            StartCoroutine(MoveToHeroGroupLeader());
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
        if(closeEnemy != null)
        {
            enemyDist = Vector3.Distance(closeEnemy.position, transform.position);
        }
        
     
    }

    IEnumerator MoveToEnemy()
    {
        if (closeEnemy != null)
        {
            fastAnim = 0.5f;
            navAgent.SetDestination(closeEnemy.position);
            yield return new WaitForSeconds(0.5f);
        }
        yield break;

    }

    IEnumerator MoveToHeroGroupLeader()
    {
        fastAnim = 0.5f;
        navAgent.SetDestination(heroGroupLeader.position);
        yield return new WaitForSeconds(0.5f);
        yield break;
    }

    IEnumerator AttackAndWait()
    {
        if (closeEnemy != null)
        {
            EnemyLogic enemyLogic;
            enemyLogic = closeEnemy.GetComponent<EnemyLogic>();
            isAttacking = true;
            fastAnim = 1.0f;
            transform.LookAt(closeEnemy);

            while (isAttacking)
            {

                yield return new WaitForSeconds(1 / attackSpeed);


                fastAnim = 0.5f;
                isAttacking = false;


                yield break;

            }
        }
        yield break;


    }

    public void Attacked() {
        EnemyLogic enemyLogic;
        enemyLogic = closeEnemy.GetComponent<EnemyLogic>();
        enemyLogic.TakeDamage(attackDamage);
    }
 
    private void AttackBegin()
    {
        canAttack = true;
    }

    private void AttackEnd()
    {
        canAttack = false;
    }




}

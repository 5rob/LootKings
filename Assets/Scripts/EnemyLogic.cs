using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[Serializable]

public class EnemyLogic : MonoBehaviour
{
    public int id;
    public string enemyName;
    public float health;
    public float damage;
    public float movementSpeed;
    public float attackSpeed;


    public GameObject hero;
    public Rigidbody rb;
    public NavMeshAgent navAgent;
    public GameManager gameManager;
    public HealthBar healthbar;
    public Canvas splat;
    public GameObject lootSpawn;

    private Animator anim;
    public GameObject particles;
    public GameObject enemySpawnerNull;
    GameObject closeEnemy;

    private float fastAnim;
    private float smoothAnim;

    public float flashTime;
    Color origionalColor;
    public SkinnedMeshRenderer mRenderer;
    public bool agro;

    public void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        healthbar = GetComponentInChildren<HealthBar>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        mRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        origionalColor = mRenderer.material.color;
        enemySpawnerNull = GameObject.FindGameObjectWithTag("EnemySpawnNull");

        //hero = GameObject.FindGameObjectWithTag("Hero");
        fastAnim = 0.0f;
        rb = GetComponent<Rigidbody>();
        healthbar.SetMaxHealth(health);
        agro = false;
    }

    private void Update()
    {
        //Movement
        float delta = fastAnim - smoothAnim;
        delta *= Time.deltaTime * 10.0f;
        smoothAnim += delta;

        anim.SetFloat("Speed", smoothAnim);



        if (agro == true)
        {
            closeEnemy = FindClosestEnemy();
            if(closeEnemy != null)
            {
                Hunt(closeEnemy);
            }
            
            
        }
    }
    /*
    public void Hunt()
    {
        float speed = movementSpeed;
        float rotSpeed = movementSpeed;

        //look at hero
        var targetRot = Quaternion.LookRotation(hero.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

        //move towards hero
        transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, Time.deltaTime * speed);

    }
    *///Hunt_Old


    public void Hunt(GameObject enemy) 
    {


        float heroDist = Vector3.Distance(enemy.transform.position, transform.position);


        if (heroDist > 2)
        {
            navAgent.SetDestination(enemy.transform.position);
            fastAnim = 0.5f;
        }
        else {
            Attack();
        }
       
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Hero");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }



    public void TakeDamage(float damage) {

        health -= damage;
        healthbar.SetHealth(health);

        mRenderer.material.color = Color.red;
        Invoke("ResetColor", 0.1f);

        //rb.AddExplosionForce(50.0f, hero.transform.position, 30.0f);

        //damage effect here

        if (health <= 0) {
            StartCoroutine(Die());
        }


    }

    public void Attack()
    {
        fastAnim = 1f;
        transform.LookAt(closeEnemy.transform);
    }

    void ResetColor()
    {
        mRenderer.material.color = origionalColor;
    }

    IEnumerator Die()
    {
        //destroy effect here
        mRenderer.enabled = false;

        var deathParticles = Instantiate(particles, transform);
        Instantiate(lootSpawn, transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        Instantiate(splat, transform.position,Quaternion.Euler(new Vector3(0,Random.Range(0,360),0)), enemySpawnerNull.transform);
        gameManager.kills += 1;
        EventManager.current.EnemyKilled();

        gameManager.enemyList.Remove(this.transform);
        yield return new WaitForSeconds(3f);

        Destroy(deathParticles);
        Destroy(gameObject);
        yield break;

    }


}
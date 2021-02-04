using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Spawner : MonoBehaviour
{
    public GameObject hero_1;
    public GameObject hero_2;
    public GameObject hero_3;
    public GameObject heroLeader;
    public List<GameObject> heroes;

    private void Start()
    {
        
        heroes.Add(hero_1);
        heroes.Add(hero_2);
        heroes.Add(hero_3);
        Vector3 posLeader = new Vector3(transform.position.x + Random.Range(-4f, 4f), 2, transform.position.z + Random.Range(-4f, 4f));
        Transform heroLeaderT = Instantiate(heroLeader.transform, posLeader, Quaternion.identity);



        for (int i = 0; 3 > i; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-4f, 4f), 2, transform.position.z + Random.Range(-4f, 4f));
            
            Transform thisHero = Instantiate(heroes[i].transform, pos, Quaternion.identity);



        }
    }
}

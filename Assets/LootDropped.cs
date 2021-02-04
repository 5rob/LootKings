using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropped : MonoBehaviour
{
    LootManager lootManager;
    public Weapon weapon;
    public string rarity;
    public int level;
    public int dmg;
    public int blk;
    public int heal;
    public int range;
    public int speed;
    public MeshRenderer meshRenderer;
    Rigidbody rb;
    GameObject lootBox;
    Animator animate;
    LootHistoryUIManager UI;


    // Start is called before the first frame update
    void Awake()
    {
        lootManager = GameObject.FindGameObjectWithTag("LootManager").GetComponent<LootManager>();
        lootBox = GameObject.FindGameObjectWithTag("LootBoxIcon");
        animate = GameObject.FindGameObjectWithTag("LootBoxIcon").GetComponent<Animator>();
        UI = GameObject.FindGameObjectWithTag("lootUI").GetComponent<LootHistoryUIManager>();

        if (Random.Range(0,100) <= 90)
        {
            weapon = lootManager.LootToDrop();
            Instantiate(weapon.prefab, this.gameObject.transform);
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            rb = GetComponentInChildren<Rigidbody>();
            Drop();
        }
        else
        {
            Destroy(this.gameObject);
        }
        

        
        
        

    }


    public void Drop() 
    {
        
        rarity = weapon.rarity;
        dmg = weapon.baseDmg;
        blk = weapon.blk;
        heal = weapon.heal;
        range = weapon.range;
        speed = weapon.speed;
        meshRenderer.material.SetColor("_EmissionColor",weapon.color);


        Vector3 force = new Vector3(Random.Range(-2f, 2f), Random.Range(3, 8), Random.Range(-2f, 2f)) * 350;
        rb.AddForce(force);

        Vector3 torque;
        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);
        rb.AddTorque(torque);
        StartCoroutine(WaitForSettle());

    }




    float lootFlySpeed = 0;
    float lootGrowSpeed = 0;
    float speedMult = 100f;
    float groMult = 0.1f;
    bool canFly = false;
    float origDist = 0;
    bool distChecked = false;
    IEnumerator WaitForSettle()
    {
        yield return new WaitForSeconds(1.2f);
        canFly = true;

    }

    private void LateUpdate()
    {
        if (canFly)
        {
            if(!distChecked)
            {
                origDist = (transform.position - lootBox.transform.position).magnitude;
                distChecked = true;
            }
            rb.useGravity = false;
            float dist = (transform.position - lootBox.transform.position).magnitude;
            Vector3 dir = (transform.position - lootBox.transform.position).normalized;
            lootFlySpeed += Time.deltaTime * speedMult;
            lootGrowSpeed += Time.deltaTime * groMult;

            float distRatio = origDist / dist;
            float distRatio2 = Mathf.Clamp((origDist / dist),1,2);

            transform.position = Vector3.MoveTowards(transform.position, lootBox.transform.position, Time.deltaTime * lootFlySpeed * distRatio);

            float clampedSpeed = Mathf.Clamp(lootGrowSpeed, 1, 3f);
            Vector3 grow = new Vector3(distRatio2,distRatio2,distRatio2);



            transform.localScale = grow;

            if (dist <= 0.5) {
                //ADD TO INVENTORY <-------------------- TODO


                //Send to loot histoy UI
                string textToSend = weapon.rarity + " " + weapon.wepName;
                UI.SendMessageToUI(textToSend, weapon.textColor);

                //animate loot icon
                animate.SetTrigger("looting");

                //destroy
                Destroy(this.gameObject);
            }
        }

    }

    



}

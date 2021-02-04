using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    public Text killCount;
    private GameManager gameManager;
    public List<GameObject> LootBoxElements;
    public bool lootBoxOpen;
    public Camera mainCam;
    public bool isPaused;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        //EventManager.current.OnEnemyKilled += UpdateKillCount;
        //UpdateKillCount();

        //Grab all LootBox UI elements and put in list
        GameObject[] lookup = GameObject.FindGameObjectsWithTag("LootBoxObject");
        for(int i = 0; lookup.Length > i; i++)
        {
            LootBoxElements.Add(lookup[i]);
        }
        lootBoxOpen = false;
        isPaused = false;
        ToggleLootBox(false);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform);
                // the object identified by hit.transform was clicked
                // do whatever you want

                if(hit.transform.gameObject.tag == "LootBoxIcon")
                {
                    if(lootBoxOpen == false)
                    {
                        ToggleLootBox(true);
                    }
                    else
                    {
                        ToggleLootBox(false);
                    }
                    
                    
                }

                if (hit.transform.gameObject.tag == "PauseIcon")
                {
                    if (isPaused)
                    {
                        Time.timeScale = 1;
                        isPaused = false;
                    }
                    else
                    {
                        Time.timeScale = 0;
                        isPaused = true;
                    }


                }

            }
        }
    }

    //Toggle mesh renderer
    public void ToggleLootBox(bool isOn)
    {
        for (int i = 0; LootBoxElements.Count > i; i++)
        {
            LootBoxElements[i].GetComponentInChildren<MeshRenderer>().enabled = isOn;
            lootBoxOpen = isOn;
        }
    }

    
        


    /*
    private void UpdateKillCount()
    {
        string killNumber = gameManager.kills.ToString();
        killCount.text = "Kills: " + killNumber;
    }
    */
}

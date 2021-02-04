using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoudiniEngineUnity;


public class GetSpawnPos : MonoBehaviour
{
    private HEU_OutputAttributesStore attribsStore;
    public HEU_OutputAttribute numData;
    public int numSpawns;
    [SerializeField]
    public List<Vector3> spawnPozzys;


    void Awake()
    {
        attribsStore = GetComponentInChildren<HEU_OutputAttributesStore>();
        numData = attribsStore.GetAttribute("numSpawns");
        numSpawns = numData._intValues[0];



        for (int i = 0; numSpawns > i; i++)
        {
            HEU_OutputAttribute temp;
            float[] tempF;
            string attName = "spawnPos_" + (i+1).ToString();

            temp = attribsStore.GetAttribute(attName);
            tempF = temp._floatValues;

            Vector3 tempV = new Vector3(tempF[0], tempF[1], tempF[2]);

            spawnPozzys.Add(tempV);

        }





    }

}

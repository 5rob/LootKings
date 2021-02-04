using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visToggle : MonoBehaviour
{
    private GameObject cam;
    private MeshRenderer ren;
    public bool side;
    public float threshold;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        ren = GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        float camRot = cam.transform.rotation.y;
        
        if (side == true)
        {
            if (camRot < threshold)
            {
                ren.enabled = false;
            }
            if (camRot > threshold)
            {
                ren.enabled = true;
            }
        }
        else 
        {
            if (camRot > threshold)
            {
                ren.enabled = false;
            }
            if (camRot < threshold)
            {
                ren.enabled = true;
            }
        }
        
    }
}
    
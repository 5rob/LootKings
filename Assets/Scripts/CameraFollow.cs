using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CameraFollow : MonoBehaviour
{
    Transform heroGroupLeader;
    public float smoothSpeed;
    public Vector3 offset;


    void Start()
    {
        heroGroupLeader = GameObject.FindGameObjectWithTag("HeroLeader").transform;

    }



    void Update()
    {
        Vector3 desiredPos = heroGroupLeader.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothPos.x, transform.position.y, smoothPos.z);

        var targetRot = Quaternion.LookRotation(heroGroupLeader.position - transform.position);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 3f * Time.deltaTime);
    }
}

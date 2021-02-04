using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    void Start()
    {
        Vector3 rot = new Vector3(transform.rotation.x, Random.rotation.y, transform.rotation.z);

        transform.rotation = Quaternion.Euler(rot);
    }


}

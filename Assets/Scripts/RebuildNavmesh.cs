using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class RebuildNavmesh : MonoBehaviour
{
    public NavMeshSurface surface;

    private void Start()
    {
        surface = GameObject.FindGameObjectWithTag("Navmesh").GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }


}

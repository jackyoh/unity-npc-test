using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class TestNavMeshBuild : MonoBehaviour {
    public NavMeshSurface navMeshSurface;


    void Start() {
       
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("Click U.");
            navMeshSurface.RemoveData();
            navMeshSurface.BuildNavMesh();
        }
    }
}

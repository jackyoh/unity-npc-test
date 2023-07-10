using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class TestNavMeshBuild : MonoBehaviour {
    public NavMeshSurface navMeshSurface;

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("Update navigation for the NavMesh.");
            navMeshSurface.RemoveData();
            navMeshSurface.BuildNavMesh();
        }
    }
}

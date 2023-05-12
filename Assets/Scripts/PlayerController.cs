using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private bool beingHandled = false;
    public GameObject prefab;
    
    void Start() {
        var position = GameObject.FindGameObjectsWithTag("Point")[0].transform.position;
        QueueProvider.playerArray = new int[5];
        for (int i = 0 ; i < 5 ; i++) {
            QueueProvider.playerArray[i] = 0;
        }
        Instantiate(prefab, position, Quaternion.identity);
        QueueProvider.playerQueue.Enqueue("player");
    }

    void Update() {
        if (!beingHandled)
            StartCoroutine(generatePlayer());
    }

    IEnumerator generatePlayer() {
        beingHandled = true;
        yield return new WaitForSeconds(3);
        var position = GameObject.FindGameObjectsWithTag("Point")[0].transform.position;
        if (QueueProvider.playerQueue.Count <= 4) {
            Instantiate(prefab, position, Quaternion.identity);
            QueueProvider.playerQueue.Enqueue("player");
        }
        beingHandled = false;
    }
}

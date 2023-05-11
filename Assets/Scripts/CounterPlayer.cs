using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterPlayer : MonoBehaviour {
    private NavMeshAgent agent;
    private NavMeshPath path;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update() {
        if (QueueProvider.counterQueue.Count > 0)
            StartCoroutine(startSleep());
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "ChargeSite1"){
            var order = QueueProvider.counterQueue.Dequeue();
            if (QueueProvider.chargeQueue[0].Count > QueueProvider.chargeQueue[1].Count) {
                QueueProvider.chargeQueue[1].Enqueue(order);
            } else {
                QueueProvider.chargeQueue[0].Enqueue(order);
            }
            StartCoroutine(chargeSleep());
        }
        if (other.gameObject.tag == "CounterSite1") {
            QueueProvider.counterStatus = "wait";
        }
    }

    IEnumerator startSleep() {
        yield return new WaitUntil(() => QueueProvider.counterStatus == "give");
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite1")[0];
        path = new NavMeshPath();
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
        GetComponent<SpriteRenderer>().flipX = true;
    }

    IEnumerator chargeSleep() {
        yield return new WaitForSeconds(1);
        GameObject site = GameObject.FindGameObjectsWithTag("CounterSite1")[0];
        path = new NavMeshPath();
        agent.SetDestination(site.transform.position);
        agent.CalculatePath(site.transform.position, path);
        GetComponent<SpriteRenderer>().flipX = false;
    }
}

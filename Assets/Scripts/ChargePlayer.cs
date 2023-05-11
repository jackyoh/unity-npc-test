using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargePlayer : MonoBehaviour {
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update() {
        if (QueueProvider.chargeQueue[0].Count > 0 || QueueProvider.chargeQueue[1].Count > 0) {
            QueueProvider.chargeStatus = "work";
            StartCoroutine(workSleep());
            QueueProvider.chargeStatus = "give";
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "ShakeSite1") {
            if (QueueProvider.chargeQueue[0].Count >= QueueProvider.chargeQueue[1].Count) {
                var order = QueueProvider.chargeQueue[0].Dequeue();
                if (QueueProvider.shakeQueue[0].Count > QueueProvider.shakeQueue[1].Count) {
                    QueueProvider.shakeQueue[1].Enqueue(order);
                } else {
                    QueueProvider.shakeQueue[0].Enqueue(order);
                }
            } else {
                var order = QueueProvider.chargeQueue[1].Dequeue();
                if (QueueProvider.shakeQueue[0].Count > QueueProvider.shakeQueue[1].Count) {
                    QueueProvider.shakeQueue[1].Enqueue(order);
                } else {
                    QueueProvider.shakeQueue[0].Enqueue(order);
                }
            }
            QueueProvider.chargeStatus = "wait";
        }
    }

    IEnumerator workSleep() {
        yield return new WaitForSeconds(3);
        StartCoroutine(giveSleep());
    }

    IEnumerator giveSleep() {
        yield return new WaitUntil(() => QueueProvider.chargeStatus == "give");
        GameObject shakeSite1 = GameObject.FindGameObjectsWithTag("ShakeSite1")[0];
        path = new NavMeshPath();
        agent.SetDestination(shakeSite1.transform.position);
        agent.CalculatePath(shakeSite1.transform.position, path);
        animator.SetBool("isUp", true);
        StartCoroutine(shakeSleep());
    }

    IEnumerator shakeSleep() {
        yield return new WaitUntil(() => QueueProvider.chargeStatus == "wait");
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite2")[0];
        animator.SetBool("isUp", false);
        path = new NavMeshPath();
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
    }
}

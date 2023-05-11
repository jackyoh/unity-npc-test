using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakePlayer : MonoBehaviour {
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator.SetBool("isUp", true);
    }

    void Update() {
        if (QueueProvider.shakeQueue[0].Count > 0 || QueueProvider.shakeQueue[0].Count > 0) {
            QueueProvider.shakeStatus = "work";
            StartCoroutine(workSleep());
            QueueProvider.shakeStatus = "give";
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "CounterSite2") {
            if (QueueProvider.shakeQueue[0].Count > QueueProvider.shakeQueue[1].Count) {
                var order = QueueProvider.shakeQueue[0].Dequeue();
                QueueProvider.resultQueue.Enqueue(order);
            } else {
                var order = QueueProvider.shakeQueue[1].Dequeue();
                QueueProvider.resultQueue.Enqueue(order);
            }
            QueueProvider.shakeStatus = "wait";
        }
        if (other.gameObject.tag == "ShakeSite2") {
            animator.SetBool("isUp", true);
            QueueProvider.shakeStatus = "wait";
        }
    }

    IEnumerator workSleep() {
        yield return new WaitForSeconds(3);
        StartCoroutine(giveSleep());
    }

    IEnumerator giveSleep() {
        yield return new WaitUntil(() => QueueProvider.shakeStatus == "give");
        GameObject counterSite2 = GameObject.FindGameObjectsWithTag("CounterSite2")[0];
        path = new NavMeshPath();
        agent.SetDestination(counterSite2.transform.position);
        agent.CalculatePath(counterSite2.transform.position, path);
        animator.SetBool("isUp", false);
        GetComponent<SpriteRenderer>().flipX = false;
        StartCoroutine(counterSleep());
    }

    IEnumerator counterSleep() {
        yield return new WaitUntil(() => QueueProvider.shakeStatus == "wait");
        GameObject shakeSite2 = GameObject.FindGameObjectsWithTag("ShakeSite2")[0];
        path = new NavMeshPath();
        agent.SetDestination(shakeSite2.transform.position);
        agent.CalculatePath(shakeSite2.transform.position, path);
        GetComponent<SpriteRenderer>().flipX = true;
    }
}

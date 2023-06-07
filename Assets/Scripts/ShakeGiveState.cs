using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeGiveState : IShakeState {
    private int waitSeconds;
    private bool arriveCounter = false;

    public ShakeGiveState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IShakeContext context) {
        if (arriveCounter) 
            context.SetState(new ShakeWaitState(1));
    }

    public void MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        GameObject counterSite2 = GameObject.FindGameObjectsWithTag("CounterSite2")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(counterSite2.transform.position);
        agent.CalculatePath(counterSite2.transform.position, path);
        animator.SetBool("isUp", false);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void Execute(IShakeContext context, string tagName) {
        if (tagName == "CounterSite2") {
            int maxCountIndex = 0;
            for (int i = 0 ; i < QueueProvider.shakeQueue.Length ; i++) {
                if (QueueProvider.shakeQueue[maxCountIndex].Count < QueueProvider.shakeQueue[i].Count) {
                    maxCountIndex = i;
                }
            }
            if (QueueProvider.shakeQueue[maxCountIndex].Count > 0) {
                var order = QueueProvider.shakeQueue[maxCountIndex].Dequeue();
                QueueProvider.resultQueue.Enqueue(order);
            }
            Debug.Log("Result Count:" + QueueProvider.resultQueue.Count);
            arriveCounter = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "give";
    }
}

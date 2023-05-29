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
            if (QueueProvider.shakeQueue[0].Count < QueueProvider.shakeQueue[1].Count) {
                if (QueueProvider.shakeQueue[1].Count > 0) {
                    QueueProvider.shakeQueue[1].Dequeue();
                }
            } else {
                if (QueueProvider.shakeQueue[0].Count > 0) {
                    QueueProvider.shakeQueue[0].Dequeue();
                }
            }
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

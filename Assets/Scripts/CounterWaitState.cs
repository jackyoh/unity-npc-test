using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterWaitState : ICounterState {
    private int waitSeconds;

    public CounterWaitState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(ICounterContext context) {
        if (QueueProvider.counterQueue.Count > 0)
            context.SetState(new CounterGiveState(1));
    }

    public void MoveCounter(GameObject gameObject, NavMeshAgent agent) {
        NavMeshPath path = new NavMeshPath();
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("CounterSite1")[0];
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void Execute(ICounterContext context, string tagName) {
        if (tagName == "CounterSite1") {
            QueueProvider.arriveCounterSite1 = true;
        }    
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "wait";
    }
}

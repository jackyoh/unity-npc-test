using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterGiveState : ICounterState {
    private int waitSeconds;
    private bool arriveCharge = false;

    public CounterGiveState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(ICounterContext context) {
        if (arriveCharge)
            context.SetState(new CounterWaitState(0));
    }

    public void MoveCounter(GameObject gameObject, NavMeshAgent agent) {
        NavMeshPath path = new NavMeshPath();
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite1")[0];
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Execute(ICounterContext context, string tagName) {
        if (tagName == "ChargeSite1") {
            QueueProvider.arriveCounterSite1 = false;
            var order = QueueProvider.counterQueue.Dequeue();
            int minCountIndex = 0;
            for (int i = 0 ; i < QueueProvider.chargeQueue.Count ; i++) {
                if (QueueProvider.chargeQueue[minCountIndex].Count > QueueProvider.chargeQueue[i].Count) {
                    minCountIndex = i;
                }
            }
            QueueProvider.chargeQueue[minCountIndex].Enqueue(order);
            arriveCharge = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "give";
    }
}

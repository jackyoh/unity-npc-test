using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeGiveState : IChargeState {
    private int waitSeconds;
    private bool arriveShake = false;

    public ChargeGiveState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IChargeContext context) {
        if (arriveShake) 
            context.SetState(new ChargeWaitState(1));
    }

    public void MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        GameObject shakeSite1 = GameObject.FindGameObjectsWithTag("ShakeSite1")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(shakeSite1.transform.position);
        agent.CalculatePath(shakeSite1.transform.position, path);
        animator.SetBool("isUp", true);
    }

    public void Execute(IChargeContext context, string tagName) {
        if (tagName == "ShakeSite1") {
            /*if (QueueProvider.chargeQueue[0].Count < QueueProvider.chargeQueue[1].Count) {
                if (QueueProvider.chargeQueue[1].Count > 0) {
                    var order = QueueProvider.chargeQueue[1].Dequeue();
                    QueueProvider.shakeQueue[1].Enqueue(order);
                }
            } else {
                if (QueueProvider.chargeQueue[0].Count > 0) {
                    var order = QueueProvider.chargeQueue[0].Dequeue();
                    QueueProvider.shakeQueue[0].Enqueue(order);
                }
            }*/
            int maxCountIndex = 0;
            for (int i = 0 ; i < QueueProvider.chargeQueue.Count ; i++) {
                if (QueueProvider.chargeQueue[maxCountIndex].Count < QueueProvider.chargeQueue[i].Count) {
                    maxCountIndex = i;
                }
            }
            if (QueueProvider.chargeQueue[maxCountIndex].Count > 0) {
                var order = QueueProvider.chargeQueue[maxCountIndex].Dequeue();
                QueueProvider.shakeQueue[maxCountIndex].Enqueue(order);
            }
            arriveShake = true;
        }
    }

    public string GetStateName() {
        return "give";
    }

    public int WaitSeconds() {
        return waitSeconds;
    }
}

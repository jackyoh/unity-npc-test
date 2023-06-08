using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeWaitState : IChargeState {
    private int waitSeconds;

    public ChargeWaitState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IChargeContext context) {
        if (QueueProvider.chargeQueue[0].Count > 0 || QueueProvider.chargeQueue[1].Count > 0)
            context.SetState(new ChargeWorkState(2));
    }

    public void MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite2")[0];
        animator.SetBool("isUp", false);
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
    }

    public void Execute(IChargeContext context, string tagName) {
        // Nothing
    }

    public string GetStateName() {
        return "wait";
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }
}

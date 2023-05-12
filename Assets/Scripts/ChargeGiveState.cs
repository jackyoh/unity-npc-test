using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeGiveState : IChargeState {

    public void UpdateState(IChargeContext context) {
        if (QueueProvider.chargeQueue[0].Count > 0 || QueueProvider.chargeQueue[1].Count > 0) {
            context.SetState(new ChargeWaitState());
        }
    }

    public IEnumerator MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        yield return new WaitUntil(() => QueueProvider.chargePlayerPosition == "ChargeSite2");
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite2")[0];
        animator.SetBool("isUp", false);
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
    }

    public void Execute(IChargeContext context) {
        // TODO
    }

    public string GetStateName() {
        return "give";
    }
}

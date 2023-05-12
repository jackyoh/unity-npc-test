using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeWorkState : IChargeState {

    public void UpdateState(IChargeContext context) {
        if (QueueProvider.chargeQueue[0].Count > 0 || QueueProvider.chargeQueue[1].Count > 0) {
            context.SetState(new ChargeGiveState(), 1);
        }
    }

    public IEnumerator MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        yield return new WaitUntil(() => QueueProvider.chargePlayerPosition == "ShakeSite1");
        GameObject shakeSite1 = GameObject.FindGameObjectsWithTag("ShakeSite1")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(shakeSite1.transform.position);
        agent.CalculatePath(shakeSite1.transform.position, path);
        animator.SetBool("isUp", true);
    }

    public void Execute(IChargeContext context) {
        // TODO
    }

    public string GetStateName() {
        return "work";
    }
}

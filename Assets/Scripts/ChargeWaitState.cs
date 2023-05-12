using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeWaitState : IChargeState {

    public void UpdateState(IChargeContext context) {
        if (QueueProvider.chargeQueue[0].Count > 0 || QueueProvider.chargeQueue[1].Count > 0)
            context.SetState(new ChargeWorkState(), 1);
    }

    public IEnumerator MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        yield return new WaitForSeconds(0);
    }

    public void Execute(IChargeContext context) {
        // Nothing
    }

    public string GetStateName() {
        return "wait";
    }
}

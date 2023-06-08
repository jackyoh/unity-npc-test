using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeWorkState : IChargeState {
    private int waitSeconds;

    public ChargeWorkState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IChargeContext context) {
        context.SetState(new ChargeGiveState(3));
    }

    public void MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        // Nothing
    }

    public void Execute(IChargeContext context, string tagName) {
        // Nothing
    }

    public string GetStateName() {
        return "work";
    }

    public int WaitSeconds() {
        return waitSeconds;
    }
}

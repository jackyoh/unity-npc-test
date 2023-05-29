using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeWorkState : IShakeState {
    private int waitSeconds;

    public ShakeWorkState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IShakeContext context) {
        context.SetState(new ShakeGiveState(3));
    }

    public void MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        // Nothing
    }

    public void Execute(IShakeContext context, string tagName) {
        // Nothing
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "work";
    }
}

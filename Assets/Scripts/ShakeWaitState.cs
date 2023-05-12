using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeWaitState : IShakeState {
    public void UpdateState(IShakeContext context) {
        if (QueueProvider.shakeQueue[0].Count > 0 || QueueProvider.shakeQueue[1].Count > 0) {
            context.SetState(new ShakeWorkState());
        }
    }

    public IEnumerator MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        yield return new WaitForSeconds(0);
    }

    public void Execute(IShakeContext context) {
        // TODO
    }

    public string GetStateName() {
        return "wait";
    }
}

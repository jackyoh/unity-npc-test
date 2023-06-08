using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeWaitState : IShakeState {
    private int waitSeconds;

    public ShakeWaitState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IShakeContext context) {
        if (QueueProvider.shakeQueue[0].Count > 0 || QueueProvider.shakeQueue[1].Count > 0) {
            context.SetState(new ShakeWorkState(2));
        }
    }

    public void MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        GameObject shakeSite2 = GameObject.FindGameObjectsWithTag("ShakeSite2")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(shakeSite2.transform.position);
        agent.CalculatePath(shakeSite2.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Execute(IShakeContext context, string tagName) {
        // Nothing
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "wait";
    }
}

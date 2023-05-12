using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeWorkState : IShakeState {

    public void UpdateState(IShakeContext context) {
        if (QueueProvider.shakeQueue[0].Count > 0 || QueueProvider.shakeQueue[1].Count > 0)
            context.SetState(new ShakeGiveState());
    }

    public IEnumerator MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        yield return new WaitUntil(() => QueueProvider.shakePlayerPosition == "CounterSite2");
        GameObject counterSite2 = GameObject.FindGameObjectsWithTag("CounterSite2")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(counterSite2.transform.position);
        agent.CalculatePath(counterSite2.transform.position, path);
        animator.SetBool("isUp", false);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;   
    }

    public void Execute(IShakeContext context) {
        // TODO
    }

    public string GetStateName() {
        return "work";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeGiveState : IShakeState {

    public void UpdateState(IShakeContext context) {
        if (QueueProvider.shakeQueue[0].Count > 0 || QueueProvider.shakeQueue[1].Count > 0) {
            context.SetState(new ShakeWaitState());
        }
    }

    public IEnumerator MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent){
        yield return new WaitUntil(() => QueueProvider.shakePlayerPosition == "ShakeSite2");
        GameObject shakeSite2 = GameObject.FindGameObjectsWithTag("ShakeSite2")[0];
        NavMeshPath path = new NavMeshPath();
        agent.SetDestination(shakeSite2.transform.position);
        agent.CalculatePath(shakeSite2.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Execute(IShakeContext context) {
        // TODO
    }

    public string GetStateName() {
        return "give";
    }
}

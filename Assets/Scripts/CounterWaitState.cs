using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterWaitState : ICounterState {

    public void UpdateState(ICounterContext context) {
        if (QueueProvider.counterQueue.Count > 0) {
            context.SetState(new CounterGiveState());
        }
    }

    public IEnumerator MoveCounter(GameObject gameObject, NavMeshAgent agent) {
        yield return new WaitForSeconds(0);
        NavMeshPath path = new NavMeshPath();
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("ChargeSite1")[0];
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Execute(ICounterContext context) {
        // Nothing
    }

    public string GetStateName() {
        return "wait";
    }
}

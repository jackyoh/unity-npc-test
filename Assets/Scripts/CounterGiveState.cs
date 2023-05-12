using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CounterGiveState : ICounterState {

    public void UpdateState(ICounterContext context) {
        if (QueueProvider.counterQueue.Count > 0) {
            context.SetState(new CounterWaitState());
        }
    }

    public IEnumerator MoveCounter(GameObject gameObject, NavMeshAgent agent) {
        yield return new WaitForSeconds(1);
        NavMeshPath path = new NavMeshPath();
        GameObject chargeSite1 = GameObject.FindGameObjectsWithTag("CounterSite1")[0];
        agent.SetDestination(chargeSite1.transform.position);
        agent.CalculatePath(chargeSite1.transform.position, path);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void Execute(ICounterContext context) {
        // TODO
    }

    public string GetStateName() {
        return "give";
    }
}

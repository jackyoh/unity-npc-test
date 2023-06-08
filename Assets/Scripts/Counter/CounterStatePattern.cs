using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface ICounterContext {
    public void SetState(ICounterState newState);
}

public class CounterStatePattern : MonoBehaviour, ICounterContext {
    private bool beingHandled = false;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private ICounterState currentState;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = new CounterWaitState(0);
    }

    void Update() {
        if (!beingHandled)
            currentState.UpdateState(this);
    }

    public void SetState(ICounterState newState) {
        currentState = newState;
        StartCoroutine(WaitSleep(currentState.WaitSeconds()));
    }

    IEnumerator WaitSleep(int seconds) {
        beingHandled = true;
        yield return new WaitForSeconds(seconds);
        currentState.MoveCounter(this.gameObject, agent);
        beingHandled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        currentState.Execute(this, other.gameObject.tag);
    }
}

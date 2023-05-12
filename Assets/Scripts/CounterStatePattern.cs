using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface ICounterContext {
    public void SetState(ICounterState newState);
}

public class CounterStatePattern : MonoBehaviour, ICounterContext {
    private NavMeshAgent agent;
    private NavMeshPath path;
    private ICounterState currentState;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = new CounterWaitState();
    }

    void Update() {
        currentState.UpdateState(this);
    }

    public void SetState(ICounterState newState) {
        StartCoroutine(currentState.MoveCounter(this.gameObject, agent));
        currentState = newState;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "ChargeSite1"){
            var order = QueueProvider.counterQueue.Dequeue();
            if (QueueProvider.chargeQueue[0].Count > QueueProvider.chargeQueue[1].Count) {
                QueueProvider.chargeQueue[1].Enqueue(order);
            } else {
                QueueProvider.chargeQueue[0].Enqueue(order);
            }
        }
    }
}

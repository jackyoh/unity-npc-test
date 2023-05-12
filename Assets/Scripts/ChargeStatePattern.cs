using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IChargeContext {
    public void SetState(IChargeState newState);
}

public class ChargeStatePattern : MonoBehaviour, IChargeContext {
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;
    private IChargeState currentState;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = new ChargeWaitState();
    }

    void Update() {
        currentState.UpdateState(this);
    }

    public void SetState(IChargeState newState) {
        StartCoroutine(RunSleep(newState, 1));
    }

    IEnumerator RunSleep(IChargeState newState, int seconds) {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(currentState.MoveCharge(this.gameObject, animator, agent));
        currentState = newState;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "ShakeSite1") {
            for (int i = 0; i < QueueProvider.chargeQueue[0].Count; i++) {
                var order = QueueProvider.chargeQueue[0].Dequeue();
                QueueProvider.shakeQueue[0].Enqueue(order);
            }
            for (int i = 0; i < QueueProvider.chargeQueue[1].Count; i++) {
                var order = QueueProvider.chargeQueue[1].Dequeue();
                QueueProvider.shakeQueue[1].Enqueue(order);
            }
            QueueProvider.chargePlayerPosition = "ChargeSite2";
        }
        
        if (other.gameObject.tag == "ChargeSite2") {
            QueueProvider.chargePlayerPosition = "ShakeSite1";
        }
    }
}

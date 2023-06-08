using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IChargeContext {
    public void SetState(IChargeState newState);
}

public class ChargeStatePattern : MonoBehaviour, IChargeContext {
    private bool beingHandled = false;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;
    private IChargeState currentState;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentState = new ChargeWaitState(0);
    }

    void Update() {
        if (!beingHandled)
            currentState.UpdateState(this);
    }

    public void SetState(IChargeState newState) {
        currentState = newState;
        StartCoroutine(WaitSleep(currentState.WaitSeconds()));
    }

    IEnumerator WaitSleep(int seconds) {
        beingHandled = true;
        yield return new WaitForSeconds(seconds);
        currentState.MoveCharge(this.gameObject, animator, agent);
        beingHandled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        currentState.Execute(this, other.gameObject.tag);
    }
}

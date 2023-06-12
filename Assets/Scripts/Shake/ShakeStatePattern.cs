using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IShakeContext {
    public void SetState(IShakeState newState);
}

public class ShakeStatePattern : MonoBehaviour, IShakeContext {
    private bool beingHandled = false;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;
    private IShakeState currentState;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        this.currentState = new ShakeWaitState(0);
        QueueProvider.shakeQueue.Add(new Queue());
        QueueProvider.shakeQueue.Add(new Queue());
    }

    void Update() {
        if (!beingHandled)
            currentState.UpdateState(this);
    }

    public void SetState(IShakeState newState) {
        currentState = newState;
        StartCoroutine(WaitSleep(currentState.WaitSeconds()));
    }

    IEnumerator WaitSleep(int seconds) {
        beingHandled = true;
        yield return new WaitForSeconds(seconds);
        currentState.MoveShake(this.gameObject, animator, agent);
        beingHandled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        currentState.Execute(this, other.gameObject.tag);
    }
}

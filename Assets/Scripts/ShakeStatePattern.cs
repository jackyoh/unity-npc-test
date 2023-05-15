using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IShakeContext {
    public void SetState(IShakeState newState);
}

public class ShakeStatePattern : MonoBehaviour, IShakeContext {
    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;
    private IShakeState currentState;

    void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        this.currentState = new ShakeWaitState();
    }

    void Update() {
        currentState.UpdateState(this);
    }

    public void SetState(IShakeState newState) {
        StartCoroutine(RunSleep(newState, 2));
    }

    IEnumerator RunSleep(IShakeState newState, int seconds) {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(currentState.MoveShake(this.gameObject, animator, agent));
        currentState = newState;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "CounterSite2") {
            QueueProvider.shakePlayerPosition = "ShakeSite2";
            for (int i = 0 ; i < QueueProvider.shakeQueue[0].Count ; i++) {
                QueueProvider.shakeQueue[0].Dequeue();
                //QueueProvider.resultQueue.Enqueue(order);
            }
            for (int i = 0 ; i < QueueProvider.shakeQueue[1].Count ; i++) {
                QueueProvider.shakeQueue[1].Dequeue();
                //QueueProvider.resultQueue.Enqueue(order);
            }
        }
        
        if (other.gameObject.tag == "ShakeSite2") {
            QueueProvider.shakePlayerPosition = "CounterSite2";
        }
    }
}

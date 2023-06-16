using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IPlayerContext {
    public void SetState(IPlayerState newState);
    public void SetOrder(Order order);
    public Order GetOrder();
    public void DestroyGameObject();
}

public class PlayerStatePattern : MonoBehaviour,  IPlayerContext{
    private bool beingHandled = false;
    private Animator animator;
    private NavMeshAgent agent;
    private IPlayerState currentState;
    private Order order;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        currentState = new PlayerBuyDrinkState(0);
    }

    void Update() {
        if (!beingHandled) {
            currentState.UpdateState(this);
        }
        currentState.MovePlayer(this.gameObject, this.animator, agent);
    }

    public void SetState(IPlayerState newState) {
        currentState = newState;
        StartCoroutine(WaitSleep(currentState.WaitSeconds()));
    }

    public void SetOrder(Order order) {
        this.order = order;
    }

    public Order GetOrder() {
        return this.order;
    }

    public void DestroyGameObject() {
        Destroy(this.gameObject);
    }

    IEnumerator WaitSleep(int waitSeconds) {
        beingHandled = true;
        yield return new WaitForSeconds(waitSeconds);
        beingHandled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        currentState.Execute(this, other.gameObject.tag, agent);
    }
}

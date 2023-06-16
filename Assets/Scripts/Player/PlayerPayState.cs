using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPayState : IPlayerState {
    private int waitSeconds;

    public PlayerPayState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
    }

    public void UpdateState(IPlayerContext context) {
        QueueProvider.numberPlate = QueueProvider.numberPlate + 1;
        Order order = new Order(QueueProvider.numberPlate, "order");
        context.SetOrder(order);
        QueueProvider.counterQueue[0].Enqueue(order);
        context.SetState(new PlayerWaitDrink(3));
    }

    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        // Nothing
    }

    public void Execute(IPlayerContext context, string tagName, NavMeshAgent agent) {
        // Nothing
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "pay";
    }
}

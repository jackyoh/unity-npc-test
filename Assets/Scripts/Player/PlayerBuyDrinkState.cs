using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBuyDrinkState : IPlayerState {
    private List<GameObject> buyDrinks = new List<GameObject>();
    private int currentWaypointIndex = 0;
    private bool arriveCounter = false;
    private bool arrivePoint = false;
    private int waitSeconds;

    public PlayerBuyDrinkState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
        for (int i = 1; i <= 5; i++) {
            buyDrinks.Add(GameObject.Find("Point" + i));
        }
    }

    public void UpdateState(IPlayerContext context) {
        if (arriveCounter) {
            context.SetState(new PlayerPayState(2));
        }
    }

    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        var direction = (buyDrinks[currentWaypointIndex].transform.position - gameObject.transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        if (arrivePoint && currentWaypointIndex < buyDrinks.Count - 1) {
            if (QueueProvider.playerArray[currentWaypointIndex + 1] == 0) {
                QueueProvider.playerArray[currentWaypointIndex + 1] = 1;
                QueueProvider.playerArray[currentWaypointIndex] = 0;

                NavMeshPath path = new NavMeshPath();
                currentWaypointIndex = currentWaypointIndex + 1;
                arrivePoint = false;
                agent.SetDestination(buyDrinks[currentWaypointIndex].transform.position);
                agent.CalculatePath(buyDrinks[currentWaypointIndex].transform.position, path);
            }
        }
    }

    public void Execute(IPlayerContext context, string tagName, NavMeshAgent agent) {
        arrivePoint = true;        
        if (tagName == "BuyDrinks") {
            arriveCounter = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "buy";
    }
}

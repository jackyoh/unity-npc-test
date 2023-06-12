using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWaitDrink : IPlayerState {
    private List<GameObject> waitDrinks = new List<GameObject>();
    private int currentWaypointIndex = 0;
    private bool arrivePoint = true;
    private bool arriveWaitDrinks = false;
    private int waitSeconds;

    public PlayerWaitDrink(int waitSeconds) {
        this.waitSeconds = waitSeconds;
        for (int i = 6 ; i <= 10 ; i++) {
            waitDrinks.Add(GameObject.Find("Point" + i));
        }
    }

    public void UpdateState(IPlayerContext context) {
        if (arriveWaitDrinks && QueueProvider.isGetDrinks) {
            context.SetState(new PlayerGetDrinkState(3));
            QueueProvider.isGetDrinks = false;
        }
    }

    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        var direction = (waitDrinks[currentWaypointIndex].transform.position - gameObject.transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if (arrivePoint && currentWaypointIndex < waitDrinks.Count - 1) {
            NavMeshPath path = new NavMeshPath();
            currentWaypointIndex = currentWaypointIndex + 1;
            arrivePoint = false;
            agent.SetDestination(waitDrinks[currentWaypointIndex].transform.position);
            agent.CalculatePath(waitDrinks[currentWaypointIndex].transform.position, path);
            
            int buyDrinkLength = QueueProvider.playerArray.Length - 1;
            QueueProvider.playerArray[buyDrinkLength] = 0;
        }
    }

    public void Execute(IPlayerContext context, string tagName) {
        arrivePoint = true;
        if (tagName == "WaitDrinks") {
            arriveWaitDrinks = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "wait-drink";
    }
}

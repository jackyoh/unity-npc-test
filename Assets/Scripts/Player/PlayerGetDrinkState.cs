using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGetDrinkState : IPlayerState {
    private List<GameObject> getDrinks = new List<GameObject>();
    private int currentWaypointIndex = 0;
    private bool arrivePoint = true;
    private bool arriveGetDrinks = false;
    private int waitSeconds;

    public PlayerGetDrinkState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
        getDrinks.Add(GameObject.Find("Point9"));
        getDrinks.Add(GameObject.Find("Point8"));
        getDrinks.Add(GameObject.Find("Point7"));
        getDrinks.Add(GameObject.Find("Point6"));
        getDrinks.Add(GameObject.Find("GetDrinks"));
    }

    public void UpdateState(IPlayerContext context) {
        if (arriveGetDrinks) {
            context.SetState(new PlayerLeaveState(2));
        }
    }

    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        var direction = (getDrinks[currentWaypointIndex].transform.position - gameObject.transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        if (arrivePoint && currentWaypointIndex < getDrinks.Count - 1) {
            NavMeshPath path = new NavMeshPath();
            currentWaypointIndex = currentWaypointIndex + 1;
            arrivePoint = false;
            agent.SetDestination(getDrinks[currentWaypointIndex].transform.position);
            agent.CalculatePath(getDrinks[currentWaypointIndex].transform.position, path);
        }
    }

    public void Execute(IPlayerContext context, string tagName) {
        arrivePoint = true;
        if (tagName == "GetDrinks") {
            arriveGetDrinks = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "getDrink";
    }
}

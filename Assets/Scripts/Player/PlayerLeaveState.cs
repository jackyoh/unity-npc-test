using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerLeaveState : IPlayerState {
    private List<GameObject> leaves = new List<GameObject>();
    private int currentWaypointIndex = 0;
    private bool arrivePoint = true;

    private int waitSeconds;


    public PlayerLeaveState(int waitSeconds) {
        this.waitSeconds = waitSeconds;
        leaves.Add(GameObject.Find("Point6"));
        leaves.Add(GameObject.Find("Point7"));
        leaves.Add(GameObject.Find("Point11"));
    }

    public void UpdateState(IPlayerContext context) {
        // Nothing
    }

    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent) {
        var direction = (leaves[currentWaypointIndex].transform.position - gameObject.transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        if (arrivePoint && currentWaypointIndex < leaves.Count - 1) {
            NavMeshPath path = new NavMeshPath();
            currentWaypointIndex = currentWaypointIndex + 1;
            arrivePoint = false;
            agent.SetDestination(leaves[currentWaypointIndex].transform.position);
            agent.CalculatePath(leaves[currentWaypointIndex].transform.position, path);
        }
    }

    public void Execute(IPlayerContext context, string tagName, NavMeshAgent agent) {
        arrivePoint = true;
        if (tagName == "DestroyPlayer") {
            context.DestroyGameObject();
            QueueProvider.playerQueue.Dequeue();
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "leave";
    }
}

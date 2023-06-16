using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class PlayerWaitDrink : IPlayerState {
    private List<GameObject> waitDrinks = new List<GameObject>();
    private int currentWaypointIndex = 0;
    private bool arrivePoint = true;
    private bool arriveWaitDrinks = false;
    private int waitIndex = -1;
    private int waitSeconds;


    public PlayerWaitDrink(int waitSeconds) {
        this.waitSeconds = waitSeconds;
        for (int i = 6 ; i <= 10 ; i++) {
            waitDrinks.Add(GameObject.Find("Point" + i));
        }
    }

    public void UpdateState(IPlayerContext context) {
        // QueueProvider.resultQueue.ToList();
        // Debug.Log(context.GetNumberPlate());        
        bool getDrink = false;
        foreach (Order order in QueueProvider.resultQueue.ToList()) {
            if (order.GetOrderId() == context.GetOrder().GetOrderId()) {
                getDrink = true;
                break;
            }
        }
        if (arriveWaitDrinks && getDrink) {
            context.SetState(new PlayerGetDrinkState(3));
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

    public void Execute(IPlayerContext context, string tagName, NavMeshAgent agent) {
        arrivePoint = true;
        if (tagName == "WaitDrinks") {
            for (int i = 0 ; i < QueueProvider.waitArray.Length ; i++) {
                if (QueueProvider.waitArray[i] == 0) {
                    waitIndex = i;
                    QueueProvider.waitArray[i] = 1;
                    break;
                }
            }
            NavMeshPath path = new NavMeshPath();
            agent.SetDestination(waitDrinks[currentWaypointIndex].transform.position + new Vector3(0, waitIndex, 0));
            agent.CalculatePath(waitDrinks[currentWaypointIndex].transform.position + new Vector3(0, waitIndex, 0), path);
            arriveWaitDrinks = true;
        }
    }

    public int WaitSeconds() {
        return this.waitSeconds;
    }

    public string GetStateName() {
        return "wait";
    }
}

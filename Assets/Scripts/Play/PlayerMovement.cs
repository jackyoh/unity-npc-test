using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private int buyDrinkWaitTime = 1;

    private NavMeshAgent agent;
    private NavMeshPath path;
    private Animator animator;

    private List<GameObject> buyDrinks = new List<GameObject>();
    private List<GameObject> waitDrinks = new List<GameObject>();
    private List<GameObject> getDrinks = new List<GameObject>();
    private List<GameObject> leaves = new List<GameObject>();
    private List<GameObject> points;

    private string pointStatus;
    private int currentWaypointIndex = 0;
    private bool arrivePoint = false;

    void Awake() {
        for (int i = 1 ; i <= 5; i++) {
            buyDrinks.Add(GameObject.Find("Point" + i));
        }
        for (int i = 6 ; i <= 10 ; i++) {
            waitDrinks.Add(GameObject.Find("Point" + i));
        }
        getDrinks.Add(GameObject.Find("Point9"));
        getDrinks.Add(GameObject.Find("Point8"));
        getDrinks.Add(GameObject.Find("Point7"));
        getDrinks.Add(GameObject.Find("Point6"));
        getDrinks.Add(GameObject.Find("GetDrinks"));

        leaves.Add(GameObject.Find("Point6"));
        leaves.Add(GameObject.Find("Point7"));
        leaves.Add(GameObject.Find("Point11"));
    }
    
    void Start() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        path = new NavMeshPath();
        points = buyDrinks;
        pointStatus = "buyDrinks";
    }

    void Update() {
        var direction = (points[currentWaypointIndex].transform.position - transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if (arrivePoint == true && currentWaypointIndex < points.Count - 1) {
            if (pointStatus == "buyDrinks") {
                if (QueueProvider.playerArray[currentWaypointIndex + 1] == 0) {
                    QueueProvider.playerArray[currentWaypointIndex + 1] = 1;
                    QueueProvider.playerArray[currentWaypointIndex] = 0;
                    currentWaypointIndex += 1;
                    arrivePoint = false;
                    agent.SetDestination(points[currentWaypointIndex].transform.position);
                    agent.CalculatePath(points[currentWaypointIndex].transform.position, path);
                }
            } else {
                currentWaypointIndex += 1;
                arrivePoint = false;
                agent.SetDestination(points[currentWaypointIndex].transform.position);
                agent.CalculatePath(points[currentWaypointIndex].transform.position, path);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        arrivePoint = true;
        if (other.gameObject.tag == "BuyDrinks") {
            StartCoroutine(buyDrinksSleep());
        }

        if (other.gameObject.tag == "WaitDrinks") {
            StartCoroutine(waiterDrinksSleep());
        }

        if (other.gameObject.tag == "GetDrinks") {
            StartCoroutine(getDrinksSleep());
        }

        if (other.gameObject.tag == "DestroyPlayer") {
            Destroy(this.gameObject);
            QueueProvider.playerQueue.Dequeue();
        }
    }
    
    IEnumerator buyDrinksSleep() {
        yield return new WaitUntil(() =>
            QueueProvider.arriveCounterSite1 == true && QueueProvider.counterQueue.Count == 0);
        StartCoroutine(payCompleted());
    }

    IEnumerator payCompleted() {
        yield return new WaitForSeconds(buyDrinkWaitTime);
        QueueProvider.counterQueue.Enqueue("order");
        QueueProvider.playerArray[currentWaypointIndex] = 0;
        points = waitDrinks;
        pointStatus = "waitDrinks";
        currentWaypointIndex = 0;
    }

    IEnumerator waiterDrinksSleep() {
        yield return new WaitForSeconds(2);
        points = getDrinks;
        pointStatus = "getDrinks";
        currentWaypointIndex = 0;
    }

    IEnumerator getDrinksSleep() {
        yield return new WaitForSeconds(1);
        points = leaves;
        pointStatus = "leaves";
        currentWaypointIndex = 0;
    }
}

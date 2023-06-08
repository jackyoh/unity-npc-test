using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface ICounterState {
    public void UpdateState(ICounterContext context);
    public void MoveCounter(GameObject gameObject, NavMeshAgent agent);
    public void Execute(ICounterContext context, string tagName);
    public int WaitSeconds();
    public string GetStateName();
}

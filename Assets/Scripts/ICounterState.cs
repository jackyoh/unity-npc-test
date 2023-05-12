using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface ICounterState {
    public void UpdateState(ICounterContext context);
    public IEnumerator MoveCounter(GameObject gameObject, NavMeshAgent agent);
    public void Execute(ICounterContext context);
    public string GetStateName();
}

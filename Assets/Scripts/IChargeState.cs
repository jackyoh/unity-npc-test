using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IChargeState {
    public void UpdateState(IChargeContext context);
    public IEnumerator MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent);
    public void Execute(IChargeContext context);
    public string GetStateName();
}

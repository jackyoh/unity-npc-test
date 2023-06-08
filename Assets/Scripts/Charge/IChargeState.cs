using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IChargeState {
    public void UpdateState(IChargeContext context);
    public void MoveCharge(GameObject gameObject, Animator animator, NavMeshAgent agent);
    public void Execute(IChargeContext context, string tagName);
    public int WaitSeconds();
    public string GetStateName();
}

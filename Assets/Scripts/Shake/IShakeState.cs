using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IShakeState {
    public void UpdateState(IShakeContext context);
    public void MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent);
    public void Execute(IShakeContext context, string tagName);
    public int WaitSeconds();
    public string GetStateName();
}

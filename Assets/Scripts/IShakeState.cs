using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IShakeState {
    public void UpdateState(IShakeContext context);
    public IEnumerator MoveShake(GameObject gameObject, Animator animator, NavMeshAgent agent);
    public void Execute(IShakeContext context);
    public string GetStateName();
}

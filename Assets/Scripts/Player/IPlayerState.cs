using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IPlayerState {
    public void UpdateState(IPlayerContext context);
    public void MovePlayer(GameObject gameObject, Animator animator, NavMeshAgent agent);
    public void Execute(IPlayerContext context, string tagName);
    public int WaitSeconds();
    public string GetStateName();
}

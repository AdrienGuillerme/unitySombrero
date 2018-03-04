using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursuit : StateMachineBehaviour {
    NavMeshAgent navMesh;
    PlayerHealth target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMesh = animator.GetComponent<NavMeshAgent>();
        navMesh.speed = 40;
        target = animator.GetComponentInChildren<PlayerInRange>().GetTarget();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target.IsDead())
            animator.SetTrigger("Patrol");
    }
}

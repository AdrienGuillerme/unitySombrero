using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursuit : StateMachineBehaviour {
    NavMeshAgent navMesh;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMesh = animator.GetComponent<NavMeshAgent>();
        navMesh.speed = 40;
    }
}

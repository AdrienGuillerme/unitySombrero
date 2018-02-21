using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour {

    private EnemyWeapon axe;
	PlayerHealth target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        axe = animator.GetComponentInChildren<EnemyWeapon>();
        axe.isAttacking = true;
		target = animator.GetComponentInChildren<PlayerInRange>().GetTarget();
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		if (target.IsDead())
			animator.SetTrigger("Patrol");
	}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		axe.isAttacking = false;
    }
}

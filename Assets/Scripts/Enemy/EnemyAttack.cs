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

		target = animator.GetComponentInChildren<PlayerInRange> ().GetTarget ();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		axe.isAttacking = false;
		if (target.IsDead ())
			animator.SetTrigger ("Patrol");
    }
}

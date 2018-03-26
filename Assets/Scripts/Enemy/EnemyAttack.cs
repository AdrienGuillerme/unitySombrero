using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour {
    EnemyWeapon weapon;
    GameObject weaponTrigger;
    private AttackTriggerCollision weaponScript;

	PlayerHealth target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weapon = animator.GetComponentInChildren<EnemyWeapon>();
        weaponTrigger = weapon.weaponTrigger;
        weaponScript = weaponTrigger.GetComponent<AttackTriggerCollision>();
        weaponTrigger.SetActive(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weaponTrigger.SetActive(false);
        weaponScript.ResetTrigger();
    }
}

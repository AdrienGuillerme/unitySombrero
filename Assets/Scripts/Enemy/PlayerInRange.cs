using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour {

    private Animator animator;
	PlayerHealth targetHealth;

    void Start () {
        animator = GetComponentInParent<Animator>();
    }

	void OnTriggerStay (Collider col) {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Pursuit") && col.gameObject.tag == "Player")
        {
			targetHealth = col.gameObject.GetComponent<PlayerHealth>();
			if (!targetHealth.IsDead()) {
				//animator.GetComponent<EnemyAttack>().SetTarget(targetHealth);
				animator.SetTrigger("Attack");
			}
        }
    }

	public PlayerHealth GetTarget(){
		return targetHealth;
	}
}

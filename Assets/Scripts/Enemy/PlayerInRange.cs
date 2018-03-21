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
        if (animator)
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("Pursuit") && col.gameObject.tag == "Player")
            {
                targetHealth = col.gameObject.GetComponent<PlayerHealth>();
                if (!targetHealth.IsDead())
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

	public PlayerHealth GetTarget(){
		return targetHealth;
	}
}
 
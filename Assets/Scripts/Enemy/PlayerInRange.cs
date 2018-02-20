using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour {

    private Animator animator;

    void Start () {
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter (Collider col) {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Pursuit") && col.gameObject.tag == "Player")
        {
            animator.SetTrigger("Attack");
        }
    }
}

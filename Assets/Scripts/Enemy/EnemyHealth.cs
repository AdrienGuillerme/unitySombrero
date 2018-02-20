using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 3;

    private int health;
    private bool isDead;

    Rigidbody enemyRigidbody;
    Animator animator;
    Vector3 knockback;

    void Start () {
        enemyRigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        isDead = false;
        health = maxHealth;
	}
	
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons")
        {
            getHurt(1);
            knockback = col.transform.forward;
            KnockBack(knockback);
        }
    }

    public void getHurt(int i)
    {
        health -= i;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");
    }

    void KnockBack(Vector3 k)
    {
        k = k * 1000;
        enemyRigidbody.AddForce(k);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 3;

    private int health;
    public bool isDead;

	public EnemiesManager enemiesManager;

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
            GetHurt(1);
            knockback = (col.transform.position - transform.position).normalized;
            knockback.y = 0;
            KnockBack(knockback);
        }
    }

	public void SetEnemiesManager(EnemiesManager m)
	{
		enemiesManager = m;
	}

    public void GetHurt(int i)
    {
        //Debug.Log("Skeleton hurt!");
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
		GameObject g = transform.parent.gameObject;
		enemiesManager.SetAgentDead (g);
    }

    void KnockBack(Vector3 k)
    {
        k = k * -50000;
        enemyRigidbody.AddForce(k);
    }
}

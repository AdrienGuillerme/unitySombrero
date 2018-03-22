using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour {

    private int health;

    public int maxHealth = 3;
    public bool isDead;

    Rigidbody enemyRigidbody;
    Animator animator;
    EnemyMove move;
    Vector3 knockback;
    public LootManager lootManager;
    public PlayerInRange playerInRange;

    void Start () {
        enemyRigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        move = animator.GetComponent<EnemyMove>();
        isDead = false;
        health = maxHealth;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons")
        {
            knockback = (col.transform.position - transform.position).normalized;
            GetHurt(1);
        }
    }

    public void GetHurt(int i)
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Damaged");
            health -= i;
            knockback.y = 0;
            KnockBack(knockback);
        }
    }

    void Die()
    {
        move.enabled = false;
        playerInRange.enabled = false;
        isDead = true;
        animator.SetTrigger("Dead");
        StartCoroutine(KillMe(2f));
    }

    IEnumerator KillMe(float delay)
    {
        yield return new WaitForSeconds(delay);
        LootOrNot();
    }

    void LootOrNot()
    {
        int goal = 5;
        if (Random.Range(0, 10) > goal) {
            lootManager.MakeSpawn(enemyRigidbody.transform.position, enemyRigidbody.gameObject.name);
        }
    }

    public void SetLootManager(LootManager manager)
    {
        lootManager = manager;
    }

    void KnockBack(Vector3 k)
    {
        k = k * -20000;
        enemyRigidbody.AddForce(k);
    }
}

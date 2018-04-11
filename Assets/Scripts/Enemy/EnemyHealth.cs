using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour {

	public AudioClip onHurt;
	public AudioClip onDie;

    public int health;

    public int maxHealth = 50;
    public bool isDead;
	public bool isBoss = false;
    public GameObject pinata;
	public EnemiesManager enemiesManager;
    public int associatedScore;

    Rigidbody enemyRigidbody;
    Animator animator;
    EnemyMove move;
    Vector3 knockback;
    public LootManager lootManager;

	private SpriteRenderer healthBar;
	private Vector3 healthScale;

    void Start () {
        enemyRigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        move = animator.GetComponent<EnemyMove>();
        isDead = false;
        health = maxHealth;
        if(isBoss)
		    healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		if(healthBar != null)
			healthScale = healthBar.transform.localScale;
	}

    void OnTriggerEnter(Collider col)
    {
        if (!isDead && col.gameObject.tag == "Weapons")
        {
            if (isBoss)
                Debug.Log("Je suis touché");
            knockback = (col.GetComponentInParent<CapsuleCollider>().transform.position - transform.position);
            knockback.y = 0;
            knockback.Normalize();
            GetHurt(20, col);
           
        }
    }

	public void SetEnemiesManager(EnemiesManager m)
	{
		enemiesManager = m;
	}

    public void GetHurt(int i, Collider col)

    {
        health -= i;
        if (health < 0)
            health = 0;

		if (isBoss)
			UpdateHealthBar ();
		
        if (health <= 0 && !isDead)
        {   
            Die();
            col.transform.GetComponentInParent<DontDestroy>().AddScore(associatedScore);
        }
        else
        {
			AudioSource.PlayClipAtPoint (onHurt, transform.position);
            animator.SetTrigger("Damaged");
            KnockBack(knockback);
        }
    }

    void Die()
    {
        move.Stop();
        enemyRigidbody.isKinematic = true;
		AudioSource.PlayClipAtPoint (onDie, transform.position);
        //move.enabled = false;
        isDead = true;
        animator.SetTrigger("Dead");
        StartCoroutine(KillMe(2f));
    }

    IEnumerator KillMe(float delay)
    {
        LootOrNot();
        yield return new WaitForSeconds(delay);
        GameObject.Destroy(this.gameObject.transform.parent.gameObject);
    }

    void LootOrNot()
    {
        if (isBoss)
        {
            pinata.SetActive(true);
            //lootManager.SpawnPinata(transform.position);
        }
        else
        {

            int rng = Random.Range(0, 100);
            if (rng > 50)
            {
                if (rng < 75)
                {
                    lootManager.MakeSpawn(transform.position, 0, 10);
                    return;
                }
                if (rng < 92)
                {
                    lootManager.MakeSpawn(transform.position, 25, 0);
                    return;
                }
                if (rng < 99)
                {
                    lootManager.MakeSpawn(transform.position, 150, 0);
                    return;
                }
                lootManager.MakeSpawn(transform.position, 300, 0);
            }
        }
    }

    public void SetLootManager(LootManager manager)
    {
        lootManager = manager;
    }

    void KnockBack(Vector3 k)
    {
        k = k * -40000;
        enemyRigidbody.AddForce(k);
    }

	public int GetCurrentHealth(){
		return health;
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.red, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}

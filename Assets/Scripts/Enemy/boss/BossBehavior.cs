using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : EnemyMove {

	public GameObject minion;
	public AudioClip[] popingSounds;
	Spawner spawner;

	int radiusArea = 15;
	float appearanceFreq = 5f;
	float timeLaps = 0f;

	EnemyHealth myHealth;
	LootManager lootManager;

	void Awake(){
		spawner = GetComponent<Spawner> ();
		myHealth = GetComponentInChildren<EnemyHealth> ();

		ChangeRanges (50f, 6f);
		//myHealth.maxHealth = 100;
		myHealth.isBoss = true;
		myHealth.associatedScore = 1000;

		lootManager = GameObject.FindGameObjectWithTag ("Lootable").GetComponent<LootManager> ();
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate ();
		if (!myHealth.isDead) {
			timeLaps += Time.deltaTime;

			if (timeLaps >= appearanceFreq) {
				float ratioHealth = (float) myHealth.GetCurrentHealth () / myHealth.maxHealth;
				if (ratioHealth <= 0.25f)
					RegularSpawns (5);
				else {
					if (ratioHealth <= 0.5f)
						RegularSpawns (3);
					else {
						if (ratioHealth <= 0.75f)
							RegularSpawns (2);
						else
							RegularSpawns (1);
					}
				}

				timeLaps = 0;
			}
		}
        
	}

	public override void AttackTrigger ()
	{
		int attNumb = Random.Range (0, 3);
		anim.SetInteger ("AttackNumber", attNumb);
		anim.SetTrigger("Attack");
	}

	void RegularSpawns(int num){
		for (int i = 0; i < num; i++) {
			Vector3 pos = transform.position + new Vector3 (Random.Range (-1.2f * radiusArea, radiusArea), 0f, 1.2f * Random.Range (-1 * radiusArea, radiusArea));
			GameObject enemy = spawner.SpawnOne (minion, pos);
			enemy.GetComponentInChildren<EnemyHealth> ().SetLootManager (lootManager);

			int x = Random.Range(0, popingSounds.Length);
			AudioSource.PlayClipAtPoint (popingSounds [x], pos);
		}
	}
}
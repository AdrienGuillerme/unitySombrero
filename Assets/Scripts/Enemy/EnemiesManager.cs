using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is here to manage the ennemies and their behavior,
 * maybe just temporarly.
 * 
 * It contains a part of the ancient "Spawner.cs" script, which has been
 * splitted to become more simple (and usable to make appear a bunch of
 * different game objects).
 */

public class EnemiesManager : MonoBehaviour{

	public GameObject agent;	// The prefab of the enemy we will make appear
	Spawner spawner;

	public bool spawnOne;		// Set it to 'true' to make 1 ennemy spawn
	public Vector3[] chosenPatrolPositions;

	public List <GameObject> dragons;
	List <Vector3> patrolPositions;
	LootManager lootManager;

	// Use this for initialization
	void Start () {
		dragons = new List<GameObject> ();
		patrolPositions = new List<Vector3> ();
		lootManager = GameObject.FindGameObjectWithTag ("Lootable").GetComponent<LootManager> ();
		spawner = GetComponent<Spawner> ();

		InitPatrolPositions ();
		spawnOne = false;
	}

	void Update() {
		if (spawnOne) {
			spawnOne = false;
			spawner.SpawnOne (agent, this.transform.position);
		}

		if (dragons.Count == 0)
			spawner.SpawnMany (agent, patrolPositions.ToArray ());

		CheckAgents ();
	}

	Vector3[] GetPatrolPosition(){
		return this.patrolPositions.ToArray ();
	}

	// If the spawner has some positions, make 1 agent spawn at each point
	// If not, spawn 3 agents at 3 pre-defined positions
	void InitPatrolPositions(){
		patrolPositions.Clear ();

		if (chosenPatrolPositions != null && chosenPatrolPositions.Length > 0)
			foreach (Vector3 v in chosenPatrolPositions)
				patrolPositions.Add (v);
		else {
			patrolPositions.Add (new Vector3 (0, 0, -10));
			patrolPositions.Add (new Vector3 (-20, 0, 3));
			patrolPositions.Add (new Vector3 (-5, 0, 18));
		}
	}

	// Make the positions rotate so the enemies can patrol correctly
	void RotatePositions(){
		Vector3 v = patrolPositions [0];
		patrolPositions.RemoveAt (0);
		patrolPositions.Add (v);
	}

	void CheckAgents(){
		GameObject[] agents = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject o in agents) {
			if (!dragons.Contains (o) && !o.GetComponentInChildren<EnemyHealth> ().isDead) {
				dragons.Add (o);
				o.GetComponentInChildren<EnemyHealth> ().SetEnemiesManager (this);
				o.GetComponent<EnemyMove> ().OnPatrol (GetPatrolPosition ());
				RotatePositions ();
			}
		}
	}

	// Check if the agent is dead and then delete him
	public void SetAgentDead(GameObject agent){
		if (dragons.Contains (agent)) {
			dragons.Remove (agent);
			StartCoroutine (KillMe (2f, agent));
		}
	}

	IEnumerator KillMe(float delay, GameObject agent){
		yield return new WaitForSeconds(delay);
		LootOrNot (agent);
		Destroy (agent);
	}

	void LootOrNot(GameObject agent){
		int goal = 5;
		if(Random.Range (0, 10) > goal)
			lootManager.MakeSpawn (agent.transform.position, agent.gameObject.name);
	}
}

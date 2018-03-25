using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour{

	public GameObject[] agentsPrefab;	// The prefab of the enemy we will make appear
	Spawner spawner;

	public bool spawnOne;		        // Set it to 'true' to make 1 ennemy spawn
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

        SpawnMany(agentsPrefab[0]);
    }

	void Update() {
		if (spawnOne) {
			spawnOne = false;
			spawner.SpawnOne (agentsPrefab[0], this.transform.position);
		}
	}

    void SpawnMany(GameObject enemyPrefab)
    {
        List<Vector3> initialPatrolList = new List<Vector3>();
        foreach (Vector3 p in patrolPositions)
        {
            initialPatrolList.Add(p);
        }
        foreach (Vector3 position in initialPatrolList)
        {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            EnemyMove move = enemy.GetComponent<EnemyMove>();
            EnemyHealth health = enemy.GetComponentInChildren<EnemyHealth>();
            move.SetPatrolPositions(patrolPositions);
            health.SetLootManager(lootManager);
            RotatePositions();
        }
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
			patrolPositions.Add (transform.position + new Vector3 (0, 0, -10));
			patrolPositions.Add (transform.position + new Vector3 (-10, 0, 10));
			patrolPositions.Add (transform.position + new Vector3 (-10, 0, 10));
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
				RotatePositions ();
			}
		}
	}

	/*// Check if the agent is dead and then delete him
	public void SetAgentDead(GameObject agent){
		if (dragons.Contains (agent)) {
			dragons.Remove (agent);
			StartCoroutine (KillMe (2f, agent));
		}
	}

	IEnumerator KillMe(float delay, GameObject agent){
		yield return new WaitForSeconds(delay);
		LootOrNot (agent.transform.position);
		Destroy (agent);
	}

	void LootOrNot(GameObject agent){
		int goal = 5;
		float rng = Random.Range (0f, 10f);
		if(rng > goal)
			lootManager.MakeSpawn (agent.transform.position, agent.gameObject.name);
	}
	*/

	void LootOrNot(Vector3 position){
		int rng = Random.Range (0, 100);
		if (rng > 50) {
			if (rng < 75) {
				lootManager.MakeSpawn (position, 0, 10);
				return;
			}
			if (rng < 92) {
				lootManager.MakeSpawn (position, 25, 0);
				return;
			}
			if (rng < 99) {
				lootManager.MakeSpawn (position, 150, 0);
				return;
			}
			lootManager.MakeSpawn (position, 300, 0);
		}
	}
}

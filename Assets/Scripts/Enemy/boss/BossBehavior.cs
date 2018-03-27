using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour {

	[SerializeField]
	Transform target;				// The purchased target

	public EnemySpawner spawner;
	int radiusArea = 10;
	int enemiesNumber = 2;
	float appearanceFreq = 5f;
	float timeLaps = 0f;

	NavMeshAgent agent;
	Vector3 initialPosition;        // The initial position... just in case the enemy has to return to
	bool goalReached = false;       // To know if we've reached our current goal or not
	bool goalChanged = true;        // To know if the goal has been changed
	int cpt = 0, freq = 50;			// Used to determine a frequence to check if the target has moved
	Vector3 goalVector;				// The goal's position
	float damping = 5f;

	void Start()
	{
		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
		initialPosition = transform.position;

		// If no defined goal, stay at the initial position
		if (target == null)
			goalVector = initialPosition;
		// If not go to the goal
		else
			SetTarget(target);
	}

	void Update()
	{
		timeLaps += Time.deltaTime;
		if (timeLaps >= appearanceFreq) {
			RegularSpawns (enemiesNumber);
			timeLaps = 0;
		}

		// If the goal has been changed, define a new destination
		if (goalChanged)
		{
			agent.SetDestination(goalVector);
			goalChanged = false;
		}

		// If the goal has been reached, just wait
		if (Vector3.Distance(goalVector, transform.position) <= agent.stoppingDistance && !goalReached)
		{
			goalReached = true;
			LookAtGoal ();
		}

		if(target != null)
			// Every 'freq' frames, we check if the target has moved
			if (cpt == freq)
				CheckTarget(target);
			else
				cpt++;
	}

	public void SetTarget(Transform goal)
	{
		goalChanged = true;
		goalReached = false;
		target = goal;
		goalVector = target.transform.position;
	}

	// Use this to check if the current target has moved
	public void CheckTarget(Transform target)
	{
		cpt = 0;
		if (target.GetComponentInChildren<PlayerHealth>().IsDead())
			GetComponent<Animator>().SetTrigger("LostTarget");
		else
			if (target.transform.position != goalVector)
				SetTarget(target);
	}

	// Use this to rotate the agent to make look at the goal
	void LookAtGoal()
	{
		Quaternion rotation = Quaternion.LookRotation(goalVector - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

	void RegularSpawns(int num){
		for (int i = 0; i < num; i++) {
			spawner.SpawnAgent (new Vector3 (Random.Range (-1 * radiusArea, radiusArea), 0f, Random.Range (-1 * radiusArea, radiusArea)));
		}
	}
}

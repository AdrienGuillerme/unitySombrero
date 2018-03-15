using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour {

    public List <Vector3> patrolPositions;	// List of positions of all the patrol's spots
	private EnemyMove enemyMove;		    // Reference to the EnemyMove script of the associated agent

	private Rigidbody enemyRigidbody;		// Reference to the rigidbody of the associated agent

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        enemyRigidbody = animator.gameObject.GetComponent<Rigidbody>();
		enemyMove = animator.gameObject.GetComponent<EnemyMove> ();

		// Check if the EnemyMove has positions for the patrol
		// If it doesn't, set positions by default
		if (enemyMove.GetPatrolPositions () == null || enemyMove.GetPatrolPositions ().Length == 0) {
			if(patrolPositions.Count == 0)
				//SetPatrolPositions();
				Debug.Log("prout");
		}

		// If it does, get these positions
		else {
			List <Vector3> positions = new List<Vector3> ();

			foreach (Vector3 v in enemyMove.GetPatrolPositions ())
				positions.Add (v);
			
			SetPatrolPositions (positions.ToArray ());
		}

		enemyMove.OnPatrol (patrolPositions.ToArray ());
    }

	// Use this to instanciate some basic positions
	public void SetPatrolPositions(){
		patrolPositions.Clear ();

		AddPatrolPosition (enemyRigidbody.transform.position);
		AddPatrolPosition (enemyRigidbody.transform.position + new Vector3 (0, 0, 20));
	}

	// Use this to instanciate an array of positions as spots for the patrol
	public void SetPatrolPositions(Vector3[] positions){
		if (positions != null && positions.Length > 0)
		{
			patrolPositions.Clear ();

			foreach (Vector3 v in positions)
				AddPatrolPosition (v);
		}
	}

	// Used to insert patrol spots in a logic way
	void AddPatrolPosition(Vector3 position){
		int numberOfElements = patrolPositions.Count;
		int index = 0;
		float minimalDistance, distancesSum;

		// We check all the possible indexes among the current spots's positions to find the good one.
		// For that, we sum the distances between the new position and its adjacent neighbors.
		if (numberOfElements > 0) {
			minimalDistance = Vector3.Distance (position, patrolPositions.ToArray () [numberOfElements - 1]) +
			Vector3.Distance (position, patrolPositions.ToArray () [0]);

			for (int i = 1; i < numberOfElements; i++) {
				distancesSum = Vector3.Distance (position, patrolPositions.ToArray () [i - 1]) +
				Vector3.Distance (position, patrolPositions.ToArray () [i]);

				if (distancesSum < minimalDistance) {
					minimalDistance = distancesSum;
					index = i;
				}
			}
		}
		patrolPositions.Insert (index, position);
	}
}

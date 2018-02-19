using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour {

    //public float patrolSpeed = 3f;
    //public float patrolDistance = 15f;

    //private Transform enemyTransform;

    //private Vector3 movement;
    //private float distanceTraveled = 0f;

	public Vector3 patrolVector1, patrolVector2;
	public EnemyMove enemyMove;

	bool positionsChanged = false;
	private Rigidbody enemyRigidbody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRigidbody = animator.gameObject.GetComponent<Rigidbody>();
		enemyMove = animator.gameObject.GetComponent<EnemyMove> ();
        //movement = enemyRigidbody.transform.forward;

		patrolVector1 = enemyRigidbody.transform.position;
		patrolVector2 = patrolVector1 + new Vector3 (0, 0, 20);

		SetPatrolVectors(patrolVector1, patrolVector2);
		enemyMove.OnPatrol (patrolVector1, patrolVector2);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		/*
		if (positionsChanged)
		{
			enemyMove.OnPatrol (patrolVector1, patrolVector2);
			positionsChanged = false;
		}
		*/

		/*
        if (distanceTraveled < patrolDistance)
        {
            Move();
        }
        else
        {
            Turn();
            distanceTraveled = 0f;
        }
        */
    }

	public void SetPatrolVectors(Vector3 position1, Vector3 position2)
	{
		// Reste à s'assurer que les positions sont accessibles : elles existent, sont sur la NavMesh etc.
		if(position1 != null && position2 != null)
			patrolVector1 = position1;
			patrolVector2 = position2;

		positionsChanged = true;
	}

	/*
    void Move()
    {
        movement = movement.normalized * patrolSpeed * Time.deltaTime;
        distanceTraveled += Vector3.Distance(new Vector3(0, 0, 0), movement);
        enemyRigidbody.MovePosition(enemyRigidbody.transform.position + movement);
    }

    void Turn()
    {
        enemyRigidbody.transform.forward = -enemyRigidbody.transform.forward;
        movement = enemyRigidbody.transform.forward;
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour {

    public float patrolSpeed = 3f;
    public float patrolDistance = 15f;

    private Rigidbody enemyRigidbody;
    private Transform enemyTransform;

    private Vector3 movement;
    private float distanceTraveled = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyRigidbody = animator.gameObject.GetComponent<Rigidbody>();
        movement = enemyRigidbody.transform.forward;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (distanceTraveled < patrolDistance)
        {
            Move();
        }
        else
        {
            Turn();
            distanceTraveled = 0f;
        }
    }

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
}

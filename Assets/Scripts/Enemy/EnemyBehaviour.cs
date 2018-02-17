using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float patrolSpeed = 3f;
    public float speedRotation = 6f;

    Vector3 movement;

    private Collider playerDetector;
    private Rigidbody enemyRigidbody;
    private Transform enemyTransform;
    private Animator animator;

    private State state;

    private bool walking;
    private float distanceTraveled = 0f;

    void Start () {
        playerDetector = GetComponent<CapsuleCollider>();
        enemyRigidbody = GetComponentInParent<Rigidbody>();
        enemyTransform = GetComponentInParent<Transform>();
        animator = GetComponentInParent<Animator>();
        movement = enemyTransform.forward;
        state = State.Patrol;
        walking = false;
    }

    void FixedUpdate () {
	    if (state == State.Patrol)
        {
            Patrol();
        }
        Animating();
	}

    void Patrol() {
        if (distanceTraveled < 15f)
        {
            walking = true;
            Move();
        }
        else
        {
            walking = false; 
            Turn();
            distanceTraveled = 0f;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Got ya");
            state = State.Chase;
        }
    }

    void Move()
    {
        walking = true;
        movement = movement.normalized * patrolSpeed * Time.deltaTime;
        distanceTraveled += Vector3.Distance(new Vector3(0, 0, 0), movement);
        enemyRigidbody.MovePosition(enemyTransform.position + movement);
    }

    void Turn()
    {
        enemyRigidbody.transform.forward = -enemyRigidbody.transform.forward;
        movement = enemyTransform.forward;
    }

    void Animating() {
        animator.SetBool("IsWalk", walking);
    }
}

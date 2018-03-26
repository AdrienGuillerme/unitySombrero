using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent agent;
    Vector3 goalPosition;
    Animator anim;
    List<Vector3> patrolPositions = new List<Vector3>();
    Collider enemyCollider;
    PlayerHealth targetHealth;

    bool goalReached = false;
	bool onPatrol = true;
    bool allowedToMove = true;
    int cpt = 0, freq = 40;			// Used to determine a frequence to check if the target has moved
    int indexPatrol;

    private float detectionRange = 25f;
    private float attackTriggerRange = 3.8f;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider>();

        InitPatrolPositions();

        if (DetectPlayers())
        {
            onPatrol = false;
            SetPlayerTarget(target);
        }
        else
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        if (allowedToMove)
        {
            if (onPatrol)
            {
                if (DetectPlayers())
                {
                    SetPlayerTarget(target);
                }
                else if (goalReached)
                {
                    goalPosition = patrolPositions[indexPatrol];
                    agent.SetDestination(goalPosition);
                    goalReached = false;
                    indexPatrol = (indexPatrol + 1) % patrolPositions.Count;
                }
                if (Vector3.Distance(goalPosition, transform.position) <= agent.stoppingDistance && !goalReached)
                {
                    goalReached = true;
                }
            }
            else
            {
                // Every 'freq' frames, we check if the target has moved
                if (cpt == freq)
                {
                    cpt = 0;
                    ChangeTarget();
                    CheckTarget(target);
                }
                else
                {
                    cpt++;
                }
                float dist = Vector3.Distance(transform.position, target.position);
                if (!targetHealth.IsDead() && dist < attackTriggerRange)
                {
                    anim.SetTrigger("Attack");
                }
            }
        }
    }

    void InitPatrolPositions ()
    {
        if (patrolPositions.Count == 0)
        {
            patrolPositions.Add(transform.position + new Vector3(0, 0, -10));
            patrolPositions.Add(transform.position + new Vector3(-10, 0, 10));
            patrolPositions.Add(transform.position + new Vector3(-10, 0, 10));
        }
    }

    public void SetPatrolPositions(List<Vector3> positions)
    {
        patrolPositions = positions;
    }

    public void SetPlayerTarget(Transform goal)
    {
        onPatrol = false;
        anim.SetTrigger("Pursuit");
        agent.speed = 8;
        goalPosition = goal.position;
        agent.SetDestination(goalPosition);
        targetHealth = goal.GetComponentInChildren<PlayerHealth>();
    }

    public void CheckTarget(Transform target)
    {
        if (targetHealth.IsDead())
        {
            Patrol();
        }
        else
        {
            Vector3 targetPosition = target.transform.position;
            if (targetPosition != goalPosition)
            {
                goalPosition = targetPosition;
                agent.SetDestination(goalPosition);
            }
        }
    }

	public void Patrol()
	{
		onPatrol = true;
		indexPatrol = 0;
        goalReached = true;
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        if (!state.IsName("Patrol"))
        {
            anim.SetTrigger("Patrol");
            agent.speed = 4;
        }
    }

    bool DetectPlayers()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, detectionRange);
        Transform newTarget = null;
        bool foundTarget = false;
        foreach (Collider col in collidersInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerHealth player = col.gameObject.GetComponent<PlayerHealth>();
                if (!player.IsDead())
                {
                    if (!foundTarget)
                    {
                        newTarget = col.gameObject.transform;
                        foundTarget = true;
                    }
                    else
                    {
                        Transform t = col.gameObject.transform;
                        float targetDistance = Vector3.Distance(t.position, transform.position);
                        float colliderDistance = Vector3.Distance(newTarget.position, transform.position);
                        if (colliderDistance < targetDistance)
                        {
                            newTarget = t;
                        }
                    }
                }
            }
        }
        target = newTarget;
        return foundTarget;
    }

    void ChangeTarget()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, detectionRange);
        Transform newTarget = null;
        float targetDistance = Vector3.Distance(transform.position, target.position);
        bool foundNewTarget = false;
        foreach (Collider col in collidersInRange)
        {
            if (col.gameObject.tag == "Player")
            {
                PlayerHealth player = col.gameObject.GetComponent<PlayerHealth>();
                if (!player.IsDead())
                {
                    float colliderDistance = Vector3.Distance(transform.position, col.transform.position);
                    if (!foundNewTarget && colliderDistance < 0.6f* targetDistance)
                    {
                        foundNewTarget = true;
                        targetDistance = colliderDistance;
                        newTarget = col.gameObject.transform;
                    }
                    else if (colliderDistance < targetDistance)
                    {
                        newTarget = col.gameObject.transform;
                    }
                }
            }
        }
        if (foundNewTarget)
        {
            target = newTarget;
        }
    }

    public void Stop()
    {
        allowedToMove = false;
        enemyCollider.isTrigger = true;
        agent.SetDestination(transform.position);
        agent.isStopped = true;
    }
}

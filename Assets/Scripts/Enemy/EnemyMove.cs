using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent agent;
    Vector3 initialPosition;
    Vector3 goalPosition;             
    Animator anim;
    List<Vector3> patrolPositions = new List<Vector3>();

    bool goalReached = false;
	bool onPatrol = true;
    int cpt = 0, freq = 50;			// Used to determine a frequence to check if the target has moved
    int indexPatrol;

    private int detectionRange = 15;

    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        initialPosition = transform.position;
        anim = GetComponent<Animator>();

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

    void Update()
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
        } else
        {
            // Every 'freq' frames, we check if the target has moved
            if (cpt == freq)
            {
                cpt = 0;
                CheckTarget(target);
            }
            else
            {
                cpt++;
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
        goalPosition = goal.position;
        agent.SetDestination(goalPosition);
    }

    public void CheckTarget(Transform target)
    {
        if (target.GetComponentInChildren<PlayerHealth>().IsDead())
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
                if (!foundTarget)
                {
                    newTarget = col.gameObject.GetComponent<Transform>();
                    foundTarget = true;
                }
                else
                {
                    Transform t = col.gameObject.GetComponent<Transform>();
                    float dist1 = Vector3.Distance(t.position, transform.position);
                    float dist2 = Vector3.Distance(newTarget.position, transform.position);
                    if (dist2 < dist1)
                    {
                        newTarget = t;
                    }
                }
            }
        }
        target = newTarget;
        return foundTarget;
    }
}

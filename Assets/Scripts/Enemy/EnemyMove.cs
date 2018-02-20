using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{	
	[SerializeField]
    Transform target;				// The purchased target

    NavMeshAgent agent;
    Vector3 initialPosition;        // The initial position... just in case the enemy has to return to
    bool goalReached = false;       // To know if we've reached our current goal or not
    bool goalChanged = true;        // To know if the goal has been changed
	bool onPatrol = false;			// To know if we're on patrol or not
    int cpt = 0, freq = 50;			// Used to determine a frequence to check if the target has moved
    Vector3 goalVector;				// The goal's position
    float damping = 5f;

	Vector3 [] patrolPositions;
	int indexPatrol;

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

		// If the goal has been changed, define a new destination
        if (goalChanged)
        {
            agent.SetDestination(goalVector);
            goalChanged = false;
        }

		// 'If' to go from a spot of our patrol to an other
		if (onPatrol && goalReached) {
			indexPatrol = (indexPatrol + 1) % patrolPositions.Length;
			goalVector = patrolPositions[indexPatrol];

			goalChanged = true;
			goalReached = false;
		}

		// If the goal has been reached, just wait
        if (Vector3.Distance(goalVector, transform.position) <= agent.stoppingDistance && !goalReached)
        {
			goalReached = true;
			//LookAtGoal ();
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
		onPatrol = false;
		target = goal;
		goalVector = target.transform.position;
    }

	// Use this to check if the current target has moved
    public void CheckTarget(Transform target)
    {
        cpt = 0;
        if (target.transform.position != goalVector)
            SetTarget(target);
    }

	// Use this to rotate the agent to make look at the goal
    void LookAtGoal()
    {
        Quaternion rotation = Quaternion.LookRotation(goalVector - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

	// Use this to set 'on' the patrol mode
	public void OnPatrol(Vector3[] positions)
	{
		target = null;
		onPatrol = true;
		goalChanged = true;

		patrolPositions = positions;

		goalVector = patrolPositions [0];
		indexPatrol = 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject agent;
    public bool spawnOne;                       // Set it to 'true' to make 1 ennemy spawn
    public Vector3[] chosenPatrolPositions;

    List<GameObject> dragons;
    List<Vector3> patrolPositions;

    void Start()
    {
        dragons = new List<GameObject>();
        patrolPositions = new List<Vector3>();

        InitPatrolPositions();
        spawnOne = false;
    }

    void Update()
    {
        if (spawnOne)
        {
            spawnOne = false;
            SpawnAgent(this.transform.position);
        }

        if (dragons.Count == 0)
            CreateWave(patrolPositions.ToArray());

        GameObject[] agents = dragons.ToArray();
        foreach (GameObject g in agents)
            IfDead(g);
    }

    // Make an ennemy appear at 'position'
    public void SpawnAgent(Vector3 position)
    {
        GameObject newAgent = (GameObject)Instantiate(agent, position, Quaternion.identity);
        newAgent.GetComponent<EnemyMove>().OnPatrol(patrolPositions.ToArray());
        dragons.Add(newAgent);
        RotatePositions();
    }

    // Use this to create a wave of 'number' enemies
    public void CreateWave(int number)
    {
        Vector3 position = this.transform.position;

        for (int i = 0; i < number; i++)
            SpawnAgent(position + new Vector3(1 * i, 0, -1 * i));
    }

    // Use this to make enemies spawn at the all the wanted positions
    public void CreateWave(Vector3[] positions)
    {
        foreach (Vector3 v in positions)
            SpawnAgent(v);
    }

    // If the spawner has some positions, make 1 agent spawn at each point
    // If not, spawn 3 agents at 3 pre-defined positions
    void InitPatrolPositions()
    {
        patrolPositions.Clear();

        if (chosenPatrolPositions != null && chosenPatrolPositions.Length > 0)
            foreach (Vector3 v in chosenPatrolPositions)
                patrolPositions.Add(v);
        else
        {
            patrolPositions.Add(new Vector3(0, 0, -10));
            patrolPositions.Add(new Vector3(-20, 0, 3));
            patrolPositions.Add(new Vector3(-5, 0, 18));
        }
    }

    // Make the positions rotate so the enemies can patrol correctly
    void RotatePositions()
    {
        Vector3 v = patrolPositions[0];
        patrolPositions.RemoveAt(0);
        patrolPositions.Add(v);
    }

    // Check if the agent is dead and then delete him
    void IfDead(GameObject agent)
    {
        if (agent.GetComponentInChildren<EnemyHealth>().isDead)
            StartCoroutine(KillMe(2f, agent));
    }

    IEnumerator KillMe(float delay, GameObject agent)
    {
        yield return new WaitForSeconds(delay);
        dragons.Remove(agent);
        Destroy(agent);
    }
}

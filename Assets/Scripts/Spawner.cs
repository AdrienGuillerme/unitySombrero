using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject agent;	// The prefab of the enemy we will make appear
	public bool spawnOne;		// Set it to 'true' to make 1 ennemy spawn

	// Use this for initialization
	void Start () {
		spawnOne = false;
	}

	void Update() {
		if (spawnOne) {
			spawnOne = false;
			SpawnAgent (this.transform.position);
		}
	}

	// Make an ennemy appear at 'position'
	void SpawnAgent(Vector3 position){
		GameObject newAgent = (GameObject)Instantiate (agent, position, Quaternion.identity);
	}

	// Use this to create a wave of 'number' enemies
	public void CreateWave(int number){
		Vector3 position = this.transform.position;

		for (int i = 0; i < number; i++) {
			SpawnAgent(position + new Vector3(1 * i, 0, -1 * i));
		}
	}

	// Use this to make enemies spawn at the all the wanted positions
	public void CreateWave(Vector3[] positions){
		for (int i = 0; i < positions.Length; i++) {
			SpawnAgent(positions[i]);
		}
	}
}

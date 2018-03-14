using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class will spawn (make appear) objects in the scene
 * When one of its methods is called.
 * 
 * Use it to spawn enemies, lootable items etc.
 */

public class Spawner : MonoBehaviour {

	float randomCoef;

	void Start(){
		randomCoef = 1f;
	}

	public void SetRandomCoef(float f){
		randomCoef = f;
	}

	// Make an object appear at 'position'
	public void SpawnOne(GameObject o, Vector3 position){
		Instantiate (o, position, Quaternion.identity);
	}

	// Make an object appear at 'position'
	// Random coef is added to the position
	public void SpawnOneRandom(GameObject o, Vector3 position){
		position += new Vector3 (Random.Range (-randomCoef, randomCoef), 0, Random.Range (-randomCoef, randomCoef));
		Instantiate (o, position, Quaternion.identity);
		
	}

	// Use this to create a set of 'number' objects
	// Random coef is added to the position for each instance of the object.
	public void SpawnMany(GameObject o, Vector3 position, int number){
		for (int i = 0; i < number; i++)
					SpawnOne (o, position + new Vector3 (Random.Range(-randomCoef, randomCoef), 0, Random.Range(-randomCoef, randomCoef)));
	}

	// Use this to make objects spawn at the all the wanted positions
	public void SpawnMany(GameObject o, Vector3[] positions){
		foreach(Vector3 v in positions)
			SpawnOne(o, v);
	}
}

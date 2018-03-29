using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class manages all the items an enemy can drop.
 * 
 * It contains a table (a dictionary) wich, for each kind of enemy,
 * associates the amount of bonus the enemy drops.
 * 
 * It makes spawn relevant items, based on the location of the enemy
 * and his associated bonuses.
 */

public class LootManager : MonoBehaviour{
	Spawner spawner;

	//Dictionary<string, string> enemiesBonuses = new Dictionary<string, string>();

	Dictionary<int, string> scoreItemsBonuses = new Dictionary<int, string>();
	Dictionary<int, string> healthItemsBonuses = new Dictionary<int, string>();

	List <int> scoresList = new List<int>();
	List <int> healthList = new List<int>();

	List <GameObject> itemsList = new List<GameObject>();

	void Start() {
		spawner = GetComponent<Spawner> ();

		foreach (Transform t in transform)
			itemsList.Add (t.gameObject);

		//CreateEnemiesDictionary ();
		CreateItemsDictionary ();
		CreateBonusesLists ();
	}

	/*
	// Here we instanciate the rewards an enemy can drop
	// The key is a string with the enemy's name,
	// The value is a string with the score and health bonuses, separated with a ';'
	void CreateEnemiesDictionary() {
		enemiesBonuses.Add ("Dragon", "75;10");
		enemiesBonuses.Add ("Skeletons_warrior", "125;10");
	}
	*/

	void CreateItemsDictionary() {
		scoreItemsBonuses.Add (150, "Emerald");
		scoreItemsBonuses.Add (25, "Ruby");
		scoreItemsBonuses.Add (300, "Diamond");

		healthItemsBonuses.Add (10, "Heart");
	}

	void CreateBonusesLists() {
		foreach (int i in scoreItemsBonuses.Keys)
			scoresList.Add (i);

		foreach (int i in healthItemsBonuses.Keys)
			healthList.Add (i);

		scoresList.Sort ();
		scoresList.Reverse ();

		healthList.Sort ();
		healthList.Reverse ();
	}

	/*
	public void MakeSpawn(Vector3 position, string EnemyName){
		int score, health;

		position += new Vector3 (0, 0.4f, 0);

		string value = "0;0";
		string[] s;
		s = EnemyName.Split ('(');
		EnemyName = s [0];
		value = enemiesBonuses[EnemyName];
		s = value.Split (';');

		score = int.Parse(s [0]);
		health = int.Parse(s [1]);

		MakeSpawn (position, score, health);
	}
	*/

	public void MakeSpawn(Vector3 position, int score, int health){
		int index = 0;
		while (score >= 0 && index < scoresList.Count) {
			if (scoresList [index] > score)
				index++;
			else {
				score -= scoresList [index];
				GameObject o;
				if ((o = GetItem (scoreItemsBonuses [scoresList [index]])) != null)
					spawner.SpawnOneRandom (o, position);
			}
		}

		index = 0;
		while (health > 0 && index < healthList.Count) {
			if (healthList [index] > health)
				index++;
			else {
				health -= healthList [index];

				GameObject o;
				if ((o = GetItem (healthItemsBonuses [healthList [index]])) != null)
					spawner.SpawnOneRandom (o, position);
			}
		}
	}

	public void SpawnPinata(Vector3 position){
        GameObject pinata = GetItem("Pinata_fbx");
        spawner.SpawnOne (pinata, position + new Vector3(0,-2,0));
        pinata.GetComponent<ItemController>().DontRotate();
	}

	GameObject GetItem(string name){
		for (int i = 0; i < itemsList.Count; i++) {
			if (itemsList[i].name == name)
				return itemsList[i];
		}
		return null;
	}

	public int[] GetItemValues(string itemName){
		int[] values = { 0, 0 };

		foreach (KeyValuePair<int, string> x in scoreItemsBonuses) {
			if (x.Value.Equals (itemName))
				values [0] = x.Key;
		}

		foreach (KeyValuePair<int, string> x in healthItemsBonuses) {
			if (x.Value.Equals (itemName))
				values [1] = x.Key;
		}

		return values;
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* This class manages the behavior of an item:
 * 
 * the item rotates until a player picks it,
 * wich give to him the bonuses this item contains.
 */

public class ItemController : MonoBehaviour {

	// Bonuses to give to the player
	public int scoreBonus = 0;
	public int healthBonus = 0;

	string itemName = "";

    bool isRotate = true;
    Vector3 goal;

    void Start() {
		goal = transform.position;

		string[] s = gameObject.name.Split ('(');
		itemName = s [0];

		LootManager lootManager = GameObject.FindGameObjectWithTag ("Lootable").GetComponent<LootManager> ();
		int[] values = lootManager.GetItemValues (itemName);

		scoreBonus = values [0];
		healthBonus = values [1];
    }

	// When on trigger with a player, look if we can give to him the bonuses
	// If we can, so we do and we then disappear
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			
			// If we only have some health to give, then we check if the player realy need it
			if (scoreBonus == 0) {
				if (col.gameObject.GetComponent<PlayerHealth> ().IsDamaged ()) {
					col.gameObject.GetComponent<PlayerHealth> ().GetHeal (healthBonus);

					Disappear ();
				}
			}
			else {
				col.gameObject.GetComponent<PlayerHealth> ().GetHeal (healthBonus);
				Debug.Log (col.gameObject.GetComponentInParent<DontDestroy> ());
				col.gameObject.GetComponentInParent<DontDestroy> ().AddScore (scoreBonus);

				Disappear ();
			}
		}
	}

	// And we turn, we turn, we turn, we turn, we *beuargh* vomit.
    void Update() {
        if(isRotate) {
            Rotation();
        }

        transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * 4);
    }

    public void MoveTo(Vector3 v) {
        isRotate = true;
        goal = v;
    }

    public void DontRotate()
    {
        isRotate = false;
    }

    void Rotation() {
        var v = new Vector3(0, 100, 0);
        this.transform.Rotate(v * Time.deltaTime);
    }

	// Use this to set the score bonus to give
	public void SetScoreBonus(int x) {
		scoreBonus = x;
	}

	// Use this to set the health bonus to give
	public void SetHealthBonus(int x) {
		healthBonus = x;
	}

	// Use this to set both score and health bonuses
	public void SetBonuses(int score, int health) {
		scoreBonus = score;
		healthBonus = health;
	}

	void Disappear(){
		Destroy (this.gameObject);
	}
}

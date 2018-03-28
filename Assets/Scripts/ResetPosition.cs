using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject[] users = GameObject.FindGameObjectsWithTag("CharacterGroup");
        int cpt = 0;
        foreach (GameObject user in users)
        {
            user.GetComponentInChildren<PlayerHealth>().ResetLife();
            user.GetComponent<Transform>().SetPositionAndRotation(new Vector3(cpt*5, 0.5f, -200f), new Quaternion(0, 0, 0, 0));
            cpt++;
        }
    }
	
}

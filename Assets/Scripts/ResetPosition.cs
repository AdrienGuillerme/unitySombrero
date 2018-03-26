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
            user.GetComponent<Transform>().SetPositionAndRotation(new Vector3(cpt, (float)0.5, cpt), new Quaternion(0, 0, 0, 0));
            user.GetComponentInChildren<PlayerHealth>().ResetLife();
        }
    }
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCapacityGlyph : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // TODO : ajouter nom du script à appeler + fonction make damage
            // other.gameObject.GetComponentInChildren<>
            Debug.Log("Glyph activated");
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCollision : MonoBehaviour {

    private Attack parent;

	// Use this for initialization
	void Awake () {
        parent = GetComponentInParent<Attack>();
	}

    private void OnTriggerEnter(Collider other)
    {
        parent.ChildTriggerEnter(other);
    }

}

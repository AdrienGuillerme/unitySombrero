using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    public float lifeTime = 2.0f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

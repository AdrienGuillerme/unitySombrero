using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public bool isCountering;
    private float shieldTime;
    private float hitTime;

    // Use this for initialization
    void Start () {
		
	}

    void Awake ()
    {
        isCountering = false;
        shieldTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons" && col.GetComponentInParent<Attack>().isAttacking)
        {
            isCountering = true;
            hitTime = Time.time;
            if(hitTime - shieldTime < 0.5)
            {
                Debug.Log("perfect counter");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isCountering = false;
    }
}

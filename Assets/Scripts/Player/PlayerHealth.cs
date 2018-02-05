using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public GameObject shield;
    private Defense defense;

    Rigidbody playerRigidbody;
    Vector3 knockback;

    void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        defense = shield.GetComponent<Defense>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons" && col.GetComponentInParent<Attack>().isAttacking)
        {
            if (defense.isCountering)
            {
                Debug.Log("I blocked an attack !");
            }
            else
            {
                Debug.Log("You hurt me!!!");
                knockback = col.transform.forward;
                KnockBack(knockback);
            }
        } 
    }

    void KnockBack(Vector3 k)
    {
        k = k * 1000;
        playerRigidbody.AddForce(k);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    Rigidbody playerRigidbody;
    Vector3 knockback;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons" && col.GetComponentInParent<Attack>().isAttacking)
        {
            Debug.Log("You hurt me!!!");
            knockback = col.transform.forward;
            KnockBack(knockback);
        }
    }

    void KnockBack(Vector3 k)
    {
        k = k * 1000;
        Debug.Log(k);
        playerRigidbody.AddForce(k);
    }
}

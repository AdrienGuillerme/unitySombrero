using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public string controllerName = "Joy1";
    public GameObject shield;
    public PlayerMovement movement;
    public bool isCountering;

    Vector3 knockback;
    Rigidbody playerRigidbody;
    private bool defending = false;
    private float shieldTime;
    private float hitTime;

    void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        isCountering = false;
        shieldTime = Time.time;
    }

    void Update()
    {
        shield.SetActive(defending);
        float lt = Input.GetAxisRaw(controllerName + "Stick3");
        if (defending)
        {
            if (lt < 0.9)
            {
                defending = false;
                movement.speed = 6;
            }
        }
        else if (lt > 0.9)
        {
            defending = true;
            movement.speed = 2;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapons" && col.GetComponentInParent<Attack>().isAttacking)
        {
            Debug.Log("Attack blocked!");
            isCountering = true;
            hitTime = Time.time;
            if (hitTime - shieldTime < 0.5)
            {
                Debug.Log("perfect counter");
            }
            knockback = col.transform.forward;
            KnockBack(knockback);
        }
    }

    void OnTriggerExit(Collider col)
    {
        isCountering = false;
    }

    void KnockBack(Vector3 k)
    {
        k = k * 500;
        playerRigidbody.AddForce(k);
    }
}

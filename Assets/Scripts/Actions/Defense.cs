using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public GameObject shield;
    public CharacterMovement movement;
    public bool isCountering;

    Vector3 knockback;
    //Rigidbody playerRigidbody;
    private bool defending = false;
    private float shieldTime;
    private float hitTime;


    private void Start()
    {
       // DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
    }

    void Awake()
    {
       // playerRigidbody = GetComponentInParent<Rigidbody>();
        isCountering = false;        
    }

    void Update()
    {
        shield.SetActive(defending);
    }

    public void ActivateDefense()
    {
        defending = true;
        movement.speed = 2;
    }

    public void DeactivateDefense()
    {
        if (defending)
        {
            defending = false;
            movement.speed = 6;
            shieldTime = Time.time * 1000;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyWeapons")
        {

            EnemyWeapon axe = col.GetComponentInParent<EnemyWeapon>();
            if (true)
            {
                Debug.Log("Attack blocked!");
                isCountering = true;
                hitTime = Time.time * 1000;
                knockback = shield.transform.forward;
                if (hitTime - shieldTime < 2000)
                {
                    Debug.Log("perfect counter");
                    knockback *= 10;
                }
                KnockBack(knockback, col.GetComponentInParent<Rigidbody>());
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        isCountering = false;
    }

    void KnockBack(Vector3 k, Rigidbody target)
    {
        k = k * 50000;
        target.AddForce(k);
    }
}

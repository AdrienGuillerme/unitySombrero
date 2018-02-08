using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulseCapacity : MonoBehaviour {

    public float repulsivePower = 7f;
    public float repulsiveRadius = 6f;
    public float repulsiveDuration = 2f;
    public float cooldownTime = 2f;

    bool inCooldown = false;
    bool inUse = false;
    float timeStamp;

    Vector3 repulsiveDir;
    string controllerName;

    private void Start()
    {
       controllerName = GetComponent<PlayerMovement>().controllerName;
    }

    private void FixedUpdate()
    {
        if (inCooldown)
        {
            if(Time.time >= timeStamp)
            {
                inCooldown = false;
            }
        }
        else if(inUse) 
        {
            Repulsive();
            if (Time.time >= timeStamp)
            {
                inUse = false;
                timeStamp = Time.time + cooldownTime;
                inCooldown = true;
            }

        }
        else
        {
                if (Input.GetButton(controllerName + "A"))
                {
                    timeStamp = Time.time + repulsiveDuration;
                    inUse = true;
                }
         }
    }

    private void Repulsive()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, repulsiveRadius);
        int i = 0;
        while(i < colliders.Length)
        {
            GameObject target = colliders[i].gameObject;
            if(target.GetComponent<Rigidbody>() != null && target != gameObject)
            {
                Debug.Log(target.GetComponent<Rigidbody>().velocity);
                Debug.Log("gameObject" + target);
                repulsiveDir = (target.transform.position - transform.position).normalized;
                target.GetComponent<Rigidbody>().AddForceAtPosition(repulsiveDir * repulsivePower / Mathf.Pow(Vector3.Distance(target.transform.position, transform.position),2f),transform.position, ForceMode.Impulse);
            }
            i++;
        }
    }


}

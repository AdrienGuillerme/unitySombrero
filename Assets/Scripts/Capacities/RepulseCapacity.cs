using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulseCapacity : MonoBehaviour {

    public float repulsivePower;
    public float repulsiveRadius;
    public float repulsiveDuration;
    public float cooldownTime;

    bool inCooldown = false;
    bool inUse = false;
    float timeStamp;

    Vector3 repulsiveDir;
    string controllerName;

    private void Start()
    {
       controllerName = GetComponentInParent<DontDestroy>().controllerName;
    }

    private void Update()
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
                if (Input.GetButtonDown(controllerName + "A"))
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
                Debug.Log("gameObject" + target);
                repulsiveDir = (target.transform.position - transform.position).normalized;
                target.transform.Translate(repulsiveDir*repulsivePower);
                
            }
            i++;
        }
    }


}

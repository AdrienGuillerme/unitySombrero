using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulseCapacity : MonoBehaviour {

    public float repulsivePower;
    public float repulsiveRadius;

    Vector3 repulsiveDir;
    string controllerName;

    private void Start()
    {
       controllerName = GetComponent<PlayerMovement>().controllerName;
    }

    private void Update()
    {
        if(Input.GetButton(controllerName + "A"))
        {
            Repulsive();
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

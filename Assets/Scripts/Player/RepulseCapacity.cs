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
        Debug.Log("start");
    }

    private void Update()
    {
        if(Input.GetButtonDown(controllerName + "A"))
        {
            Debug.Log("buttonA");
            Repulsive();
        }
    }

    private void Repulsive()
    {
        Debug.Log("répulsion");
        Collider[] colliders = Physics.OverlapSphere(transform.position, repulsiveRadius);
        int i = 0;
        while(i < colliders.Length)
        {
            GameObject target = colliders[i].gameObject;
            if(target.GetComponent<Rigidbody>() != null && target != gameObject)
            {
                Debug.Log("gameObject" + colliders[i].gameObject);
                repulsiveDir = colliders[i].transform.position - transform.position;
                colliders[i].transform.Translate(repulsiveDir);
            }
            i++;
        }
    }


}

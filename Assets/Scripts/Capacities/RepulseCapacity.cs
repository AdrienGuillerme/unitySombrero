using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulseCapacity : MonoBehaviour, ICapacity
{

    private bool activated, triggered;
    private SphereCollider col;
    // Use this for initialization
    void Start()
    {
        activated = false;
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateCapacity()
    {
        if (!activated)
        {
            activated = true;
            GameObject obj = Resources.Load("RepulseEffect") as GameObject;
            Transform transformEffect = transform;
            transformEffect.Rotate(new Vector3(1, 0, 0), 90);
            GameObject freezeEffect = Instantiate(obj, transform.position, transform.rotation);
            col.radius *= 10;
        }
    }
}


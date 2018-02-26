using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCollision : MonoBehaviour {

    private AttackThief parent;
    private MeshCollider selfCollider;
    private Vector3 initPos;
    private Quaternion initRot;

    // Use this for initialization
    void Start () {
        parent = GetComponentInParent<AttackThief>();
        selfCollider = GetComponentInParent<MeshCollider>();
        initPos = transform.localPosition;
        initRot = transform.localRotation;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other != selfCollider)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ResetTrigger()
    {
        this.transform.localPosition = initPos;
        this.transform.localRotation = initRot;
    }

}

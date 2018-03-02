using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerCollision : MonoBehaviour {

    private Attack parent;
    private MeshCollider selfCollider;
    private Vector3 initPos;
    private Vector3 initPosWorld;
    private Quaternion initRot;

    // Use this for initialization
    void Start () {
        parent = GetComponentInParent<Attack>();
        selfCollider = GetComponentInParent<MeshCollider>();
        initPos = transform.localPosition;
        initPosWorld = transform.position;
        initRot = transform.localRotation;
	}

    private void OnEnable()
    {
        initPosWorld = transform.position;
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

    public float PosDiffFromStart()
    {
        return (this.transform.position - initPosWorld).magnitude;
    }

}

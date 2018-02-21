 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject weapon;
    public bool isAttacking;
    

    private Transform weaponTransform;
    private bool isPressingTrigger;
    private Vector3 initPos;
    private Vector3 targetPos;
    private Vector3 target;
    private string controllerName;

    private float smoothTime = 0.05F;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        weaponTransform = weapon.GetComponent<Transform>();
        isAttacking = false;
        isPressingTrigger = false;
        initPos = weaponTransform.localPosition;
        targetPos = new Vector3(-1, 0, 0);
        target = initPos;

        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
    }

    void Update () {
        if (Input.GetAxis(controllerName + "Stick3") < -0.8f && !isAttacking && !isPressingTrigger)
        {
            weapon.SetActive(true);
            isAttacking = true;
            isPressingTrigger = true;
            target = initPos + targetPos;
            StartCoroutine(ProceedAttack(0.2f));        // Set attack cooldown here
        }
        else if (Input.GetAxis(controllerName + "Stick3") > -0.7f && isPressingTrigger)
        {
            isPressingTrigger = false;
        }
        weaponTransform.localPosition = Vector3.SmoothDamp(weaponTransform.localPosition, target, ref velocity, smoothTime);
    }

    IEnumerator ProceedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetTrigger();
        isAttacking = false;
    }

    public void ChildTriggerEnter(Collider other)
    {
        ResetTrigger();
        //isAttacking = false;
    }

    void ResetTrigger()
    {
        target = initPos;
        weapon.SetActive(false);
        weaponTransform.localPosition = target;
    }
}

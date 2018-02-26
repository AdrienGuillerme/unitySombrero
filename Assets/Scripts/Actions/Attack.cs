 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject weapon;
    public bool isAttacking;
    

    private Transform weaponTransform;
    private Vector3 initPos;
    private Vector3 targetPos;
    private Vector3 target;

    private float smoothTime = 0.05F;
    private Vector3 velocity = Vector3.zero;

    void Start () {
        weaponTransform = weapon.GetComponent<Transform>();
        isAttacking = false;

        initPos = weaponTransform.localPosition;
        targetPos = new Vector3(-1, 0, 0);
        target = initPos;

        //init gameController
       
    }

    void Update () {
        weaponTransform.localPosition = Vector3.SmoothDamp(weaponTransform.localPosition, target, ref velocity, smoothTime);
    }


    public void DoAttack()
    {
        if (!isAttacking)
        {
            weapon.SetActive(true);
            isAttacking = true;
            target = initPos + targetPos;
            StartCoroutine(ProceedAttack(0.2f));
        }
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

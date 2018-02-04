using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject weapon;
    public bool isAttacking;
    public string controllerName = "Joy1";

    private Transform weaponTransform;
    private bool isPressingTrigger;
    private Vector3 target;

    void Start () {
        weaponTransform = weapon.GetComponent<Transform>();
        isAttacking = false;
        isPressingTrigger = false;
        target = Vector3.zero;
    }

    void Update () {
        weapon.SetActive(isAttacking);
        if (Input.GetAxis(controllerName + "Stick3") < -0.8f && !isAttacking && !isPressingTrigger)
        {
            isAttacking = true;
            isPressingTrigger = true;
            StartCoroutine(WaitAttackEnd(0.2f));
        }
        else if (Input.GetAxis(controllerName + "Stick3") > -0.7f && isPressingTrigger)
        {
            isPressingTrigger = false;
        }
    }

    IEnumerator WaitAttackEnd(float delay)
    {
        target = new Vector3(0, 0, 0.1f);
        for(float f = 0; f < 9; f += 1)
        {
            weaponTransform.Translate(target);
        }
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        weaponTransform.Translate(new Vector3(0, 0, -0.9f));
    }
}

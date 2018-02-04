using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public GameObject weapon;
    public bool isAttacking;
    public string controllerName = "Joy1";

    private bool isPressingTrigger;
    private int amp;
    private Vector3 target;

    void Start () {
        isAttacking = false;
        isPressingTrigger = false;
        target = Vector3.zero;
        amp = 1;
    }

    void Update () {
        weapon.SetActive(isAttacking);
        if (Input.GetAxis(controllerName + "Stick3") < -0.8f && !isAttacking && !isPressingTrigger)
        {
            isAttacking = true;
            isPressingTrigger = true;
            amp = 3;
            StartCoroutine(WaitAttackEnd(0.2f));
        }
        else if (Input.GetAxis(controllerName + "Stick3") > -0.7f && isPressingTrigger)
        {
            isPressingTrigger = false;
        }
    }

    IEnumerator WaitAttackEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
        amp = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public Transform weapon;
    public bool isAttacking;
    private bool isPressingTrigger;
    private int amp;
    private Vector3 target;

    // Use this for initialization
    void Start () {
        isAttacking = false;
        isPressingTrigger = false;
        target = Vector3.zero;
        amp = 1;
    }

    // Update is called once per frame
    void Update () {
        Move(weapon.parent.position + Vector3.RotateTowards(weapon.parent.forward, weapon.forward, 100, 100) * amp);
        if (Input.GetAxis("Joy1Stick3") < -0.8f && !isAttacking && !isPressingTrigger)
        {
            Debug.Log("input on");
            isAttacking = true;
            isPressingTrigger = true;
            amp = 3;
            StartCoroutine(WaitAttackEnd(0.2f));
        }
        else if (Input.GetAxis("Joy1Stick3") > -0.7f && isPressingTrigger)
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

    void Move(Vector3 pos)
    {
        weapon.position = pos;
    }

    void Hit()
    {

    }

}

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackThief : MonoBehaviour {
    public GameObject weaponTriggerL;
    public GameObject weaponTriggerR;
    private AttackTriggerCollision weaponScriptL;
    private AttackTriggerCollision weaponScriptR;

    public bool isAttacking;

    private Animator anim;
    public bool isMovable;

    private bool isPressingTrigger;
    private string controllerName;

    void Start () {
        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;

        weaponScriptL = weaponTriggerL.GetComponent<AttackTriggerCollision>();
        weaponScriptR = weaponTriggerR.GetComponent<AttackTriggerCollision>();

        isAttacking = false;
        isPressingTrigger = false;
        isMovable = true;
        weaponTriggerR.SetActive(false);
        weaponTriggerL.SetActive(false);
        anim = GetComponentInParent<Animator>();

    }

    void Update () {
        if (Input.GetAxis(controllerName + "Stick3") < -0.8f && !isAttacking && !isPressingTrigger)
        {
            isAttacking = true;
            isPressingTrigger = true;
            anim.SetFloat("attackNumber", 0);
            anim.SetTrigger("attack");
            StartCoroutine(ChangeWeaponActive(0.2f, true));
        }
        else if (Input.GetAxis(controllerName + "Stick3") > -0.7f && isPressingTrigger)
        {
            isPressingTrigger = false;
        }
    }

    IEnumerator ChangeWeaponActive(float delay, bool state)
    {
        yield return new WaitForSeconds(delay);
        weaponTriggerR.SetActive(state);
        weaponTriggerL.SetActive(state);
    }

    void AnimationEnter(AnimatorStateInfo stateInfo)
    {
        if (stateInfo.IsName("Attack_Tree"))
        {
            isMovable = false;
        }
    }

    void AnimationExit(AnimatorStateInfo stateInfo)
    {
        if (true) //isAlive
        {
            if (stateInfo.IsName("Attack_Tree"))
            {
                isMovable = true;
                anim.ResetTrigger("damaged");
                StartCoroutine(ChangeWeaponActive(0.0f, false));
                weaponScriptL.ResetTrigger();
                weaponScriptR.ResetTrigger();
                isAttacking = false;
            }
        }
    }
}

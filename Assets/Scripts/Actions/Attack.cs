 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public AudioClip attackSword;

    public GameObject weaponTriggerL;
    public GameObject weaponTriggerR;
    private AttackTriggerCollision weaponScriptL;
    private AttackTriggerCollision weaponScriptR;

    private Animator anim;

    public bool isAttacking;
    private bool isPressingTrigger;
    private string controllerName;

    void Start () {
        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;

        weaponScriptL = weaponTriggerL.GetComponent<AttackTriggerCollision>();
        weaponScriptR = weaponTriggerR.GetComponent<AttackTriggerCollision>();

        isAttacking = false;
        weaponTriggerR.SetActive(false);
        weaponTriggerL.SetActive(false);
        anim = GetComponentInParent<Animator>();
    }

    void Update () {
        
    }


    public void DoAttack()
    {
        if (!isAttacking)
        {
			AudioSource.PlayClipAtPoint (attackSword, transform.position);
            isAttacking = true;
            //anim.SetFloat("attackNumber", 0.2f);
            anim.SetTrigger("attack");
            StartCoroutine(ChangeWeaponActive(0.2f, true));
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

    }

    void AnimationExit(AnimatorStateInfo stateInfo)
    {
        if (true) //on peut vérifier isAlive au besoin
        {
            if (stateInfo.IsName("Attack_Tree"))
            {
                anim.ResetTrigger("damaged");
                StartCoroutine(ChangeWeaponActive(0.0f, false));
                weaponScriptL.ResetTrigger();
                weaponScriptR.ResetTrigger();
                isAttacking = false;
            }
        }
    }

    //For access by Trigger Script
    public void DisableWeapons()
    {
        StartCoroutine(ChangeWeaponActive(0.0f, false));
    }
}

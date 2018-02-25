using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {


    private string controllerName;
    Attack attackScript;
    Resurection resurectionScript;
    LaunchCapacity capacityScript;
    Defense defenseScript;
	// Use this for initialization
	void Start () {

        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        attackScript = this.transform.Find("Sword").GetComponent<Attack>();
        resurectionScript = this.transform.Find("ReviveCollider").GetComponent<Resurection>();
        capacityScript = GetComponentInChildren<LaunchCapacity>();
        defenseScript = GetComponentInChildren<Defense>();
        controllerName = parentFunction.controllerName;
    }
	
	// Update is called once per frame
	void Update () {

        //Attaque
        if (Input.GetAxis(controllerName + "Stick3") < -0.8f)
        {
            /*
            weapon.SetActive(true);
            isAttacking = true;
            isPressingTrigger = true;
            target = initPos + targetPos;
            StartCoroutine(ProceedAttack(0.2f));        // Set attack cooldown here
            */   
        }
        else if (Input.GetAxis(controllerName + "Stick3") > -0.7f)
        {
           // isPressingTrigger = false;
        }
        else if (Input.GetButton(controllerName + "Action"))
        {
            resurectionScript.Revive();
        }
        else if (Input.GetButtonDown(controllerName + "LaunchCapacity"))
        {
            capacityScript.Launch();
        }
    
        if (Input.GetAxisRaw(controllerName + "Stick3") < 0.9)
        {
            defenseScript.DeactivateDefense();
        }
        else if (Input.GetAxisRaw(controllerName + "Stick3") > 0.9)
        {
            defenseScript.ActivateDefense();
        }


    }
}

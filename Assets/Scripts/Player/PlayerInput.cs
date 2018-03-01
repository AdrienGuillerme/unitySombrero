using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {


    private string controllerName;
    Attack attackScript;
    Resurection resurectionScript;
    LaunchCapacity capacityScript;
    Defense defenseScript;
    private bool isActing;
	// Use this for initialization
	void Start () {
        isActing = false;
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
        if (!isActing)
        {
            //Attaque
            if (Input.GetAxis(controllerName + "Stick3") < -0.8f)
            {
                attackScript.DoAttack();
                StartCoroutine(DoAction(0.2f));
            }
            else if (Input.GetButton(controllerName + "Action"))
            {
                resurectionScript.Revive();
                StartCoroutine(DoAction(0.1f));
            }
            else if (Input.GetButtonDown(controllerName + "LaunchCapacity"))
            {
                capacityScript.Launch();
                StartCoroutine(DoAction(0.1f));
            }
            else if (Input.GetAxisRaw(controllerName + "Stick3") > 0.9)
            {
                defenseScript.ActivateDefense();
                StartCoroutine(DoAction(0.2f));
            }
        }

        if (Input.GetAxisRaw(controllerName + "Stick3") < 0.9)
        {
            defenseScript.DeactivateDefense();
        }
    }

    IEnumerator DoAction(float delay)
    {
        isActing = true;
        yield return new WaitForSeconds(delay);
        isActing = false;
    }
}

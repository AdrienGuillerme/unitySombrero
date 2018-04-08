using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCapacity : MonoBehaviour {

    string controllerName;
    public CapacityEnum capacityIntChosen;
    public float orbeAltitude = 3f;
    public float coolDownTime = 3f;

    private GameObject launcher;
    private float distanceToCharacter = 2f;
    private bool readyToLaunch;
    private bool inCoolDown;

    private MeshRenderer sombrero;

    // Use this for initialization
    void Start () {
        controllerName = GetComponentInParent<DontDestroy>().controllerName;
        int cpt = GetComponentInParent<DontDestroy>().cptCapacity;
        capacityIntChosen = (CapacityEnum)cpt;
        Debug.Log(capacityIntChosen);
        readyToLaunch = true;
        inCoolDown = false;
        sombrero = this.transform.Find("Player/rig/spine/chest/neck/head/MexicanHat").GetComponent<MeshRenderer>();
        sombrero.material = Resources.Load(capacityIntChosen.ToString(), typeof(Material)) as Material;
        
    }
	
	// Update is called once per frame
	void Update () {
         //Vector3 repulsiveDir = (this.transform.position - character.transform.position).normalized;
        //this.transform.Translate(repulsiveDir * powerImpulsion);
    }

    public void Launch()
    {
		bool isDead = GetComponentInChildren<PlayerHealth>().IsDead();
		
		if (launcher == null)
            readyToLaunch = true;
		
        if(!isDead)
        {
            if (readyToLaunch)
            {
                if(!inCoolDown)
                {
                    GameObject obj = Resources.Load("CapacityLauncher") as GameObject;
                    Vector3 position = transform.forward * distanceToCharacter + transform.position;
                    position.y += orbeAltitude;
                    launcher = Instantiate(obj, position, transform.rotation) as GameObject;
                    launcher.GetComponentInChildren<LauncherCapacityBehaviour>().SetParent(this);
                    launcher.GetComponentInChildren<LauncherCapacityBehaviour>().SetPlayerCollider(GetComponent<CapsuleCollider>() as CapsuleCollider);
                    launcher.GetComponentInChildren<LauncherCapacityBehaviour>().SetAltitude(orbeAltitude);
                    readyToLaunch = false;
                    StartCoroutine(CoolDown());
                }
            }
            else
            {
                launcher.GetComponentInChildren<LauncherCapacityBehaviour>().activate();
                launcher = null;
                readyToLaunch = true;
            }
        }
    }

    public string getControllerName()
    {
        return this.controllerName;
    }

    public CapacityEnum getCapacity()
    {
        return this.capacityIntChosen;
    }


    public void SetCapacity(CapacityEnum capacity)
    {
        this.capacityIntChosen = capacity;
    }

    IEnumerator CoolDown()
    {
        inCoolDown = true;
        yield return new WaitForSeconds(coolDownTime);
        inCoolDown = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCapacity : MonoBehaviour {

    string controllerName;
    public CapacityEnum capacityIntChosen = CapacityEnum.Glyph;
    private float distanceToCharacter = 2f;
    public float orbeAltitude = 0.5f;
    private GameObject launcher;
    private bool readyToLaunch;

    // Use this for initialization
    void Start () {
        controllerName = GetComponentInParent<DontDestroy>().controllerName;
        readyToLaunch = true;
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
		
		if(readyToLaunch && !isDead)
		{
			GameObject obj = Resources.Load("CapacityLauncher") as GameObject;
			Vector3 position = transform.forward * distanceToCharacter + transform.position;
			position.y += orbeAltitude;
			launcher = Instantiate(obj, position, transform.rotation) as GameObject;
			launcher.GetComponentInChildren<LauncherCapacityBehaviour>().setParent(this);
			launcher.GetComponentInChildren<LauncherCapacityBehaviour>().SetAltitude(orbeAltitude);
			//Instantiate(obj, transform);
			readyToLaunch = false;
		}
		else
		{
			launcher.GetComponentInChildren<LauncherCapacityBehaviour>().activate();
			launcher = null;
			readyToLaunch = true;
		}
    }

    public string getControllerName()
    {
        return this.controllerName;
    }

    public void SetCapacity(CapacityEnum capacity)
    {
        this.capacityIntChosen = capacity;
    }
}

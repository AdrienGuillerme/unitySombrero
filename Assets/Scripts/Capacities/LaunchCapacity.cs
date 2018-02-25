using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCapacity : MonoBehaviour {

    string controllerName;
    public enum Capacity { Glyph, Repulsion };
    public Capacity capacityIntChosen = Capacity.Glyph;
    float distanceToCharacter = 1f;
    private GameObject launcher;
    private bool asBeenLaunched;

    // Use this for initialization
    void Start () {
        controllerName = GetComponentInParent<DontDestroy>().controllerName;
        asBeenLaunched = false;
    }
	
	// Update is called once per frame
	void Update () {
         //Vector3 repulsiveDir = (this.transform.position - character.transform.position).normalized;
        //this.transform.Translate(repulsiveDir * powerImpulsion);
    }

    public void Launch()
    {
        if (!asBeenLaunched)
        {
            GameObject obj = Resources.Load("CapacityLauncher") as GameObject;
            Vector3 position = transform.forward * distanceToCharacter + transform.position;
            launcher = Instantiate(obj, position, transform.rotation) as GameObject;
            launcher.GetComponentInChildren<LauncherCapacityBehaviour>().setParent(this);
            //Instantiate(obj, transform);
            asBeenLaunched = true;
        }
        else
        {
            launcher.GetComponentInChildren<LauncherCapacityBehaviour>().activate();
            launcher = null;
            asBeenLaunched = false;
        }
    }

    public string getControllerName()
    {
        return this.controllerName;
    }
}

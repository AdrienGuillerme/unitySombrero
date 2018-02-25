using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherCapacityBehaviour : MonoBehaviour {

    float speed = 0.10f;
    private string controllerName;
    private LaunchCapacity.Capacity capacityIntChosen = LaunchCapacity.Capacity.Glyph;
    private ICapacity capacityChosen;
    private LaunchCapacity parentFunction;
    private bool activated;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        //init gameController

        //LauchCapacity parentFunction = GetComponentInParent<LauchCapacity>();
        controllerName = parentFunction.getControllerName();
        capacityIntChosen = parentFunction.capacityIntChosen;
        activated = false;

        switch (capacityIntChosen)
        {
            case LaunchCapacity.Capacity.Glyph:
                capacityChosen = GetComponentInChildren<GlyphCapacity>();
                break;
            case LaunchCapacity.Capacity.Repulsion:
                capacityChosen = GetComponentInChildren<RepulseCapacity>();
                break;
            default:
                Debug.Log("Default case");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(activated) {
            capacityChosen.ActivateCapacity();
            Destroy(gameObject);
        }

        transform.position += speed * transform.forward;
    }

    public void activate()
    {
        activated = true;
    }

    public void setParent(LaunchCapacity parent)
    {
        this.parentFunction = parent;
    }
}

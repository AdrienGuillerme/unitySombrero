using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherCapacityBehaviour : MonoBehaviour {

    float speed = 0.10f;
    private string controllerName;
    private LauchCapacity.Capacity capacityIntChosen = LauchCapacity.Capacity.Glyph;
    private ICapacity capacityChosen;
    private LauchCapacity parentFunction;
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
            case LauchCapacity.Capacity.Glyph:
                capacityChosen = GetComponentInChildren<GlyphCapacity>();
                break;
            case LauchCapacity.Capacity.Repulsion:
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

    public void setParent(LauchCapacity parent)
    {
        this.parentFunction = parent;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherCapacityBehaviour : MonoBehaviour {

    public float speed = 10f;
    private string controllerName;
    private CapsuleCollider playerCollider;
    private CapacityEnum capacityIntChosen = CapacityEnum.Glyph;
    private ICapacity capacityChosen;
    private LaunchCapacity parentFunction;
    private bool activated;
    private Rigidbody rb;
    private float altitude;

    // Use this for initialization
    void Start () {
        //init gameController

        //LauchCapacity parentFunction = GetComponentInParent<LauchCapacity>();
        controllerName = parentFunction.getControllerName();
        capacityIntChosen = parentFunction.capacityIntChosen;
        activated = false;

        switch (capacityIntChosen)
        {
            case CapacityEnum.Glyph:
                capacityChosen = GetComponentInChildren<GlyphCapacity>();
                break;
            case CapacityEnum.Repulsion:
                capacityChosen = GetComponentInChildren<RepulseCapacity>();
                break;
            case CapacityEnum.Meteor:
                capacityChosen = GetComponentInChildren<MeteorCapacity>();
                break;
            case CapacityEnum.Freeze:
                capacityChosen = GetComponentInChildren<FreezeCapacity>();
                break;
            default:
                Debug.Log("Default case");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {

        // Altitude detection so the orbe stays at a constant altitude from the ground
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit))
        {
            if (hit.collider.tag != "Glyph")
            {
                float height = hit.distance;
                if (height != altitude)
                {
                    transform.Translate(new Vector3(0, altitude - height, 0));
                }
            }

        }

        if (activated) {
            capacityChosen.ActivateCapacity();

            if (capacityIntChosen != CapacityEnum.Freeze && capacityIntChosen != CapacityEnum.Repulsion)
                Destroy(gameObject);
            else
            {
                speed = 0;
                Destroy(gameObject, 0.2f);
            }
                
        }

        transform.position += speed * transform.forward * Time.deltaTime;
    }

    public void activate()
    {
        activated = true;
    }

    public void SetParent(LaunchCapacity parent)
    {
        this.parentFunction = parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemi")
        {
            activate();
        }
        else if (other.gameObject.tag == "Player" && !other.gameObject.GetComponentInParent<LaunchCapacity>().getControllerName().Equals(this.controllerName))
        {
            activate();
        }
        else if (other.gameObject.tag.Equals("EnvironmentComponent"))
        {
            Destroy(this.gameObject, 0.2f);
        }
    }

    public void SetAltitude(float altitude)
    {
        this.altitude = altitude;
    }

    public float GetAltitude()
    {
        return this.altitude;
    }

    public string GetControllerName()
    {
        return this.controllerName;
    }

    public CapsuleCollider GetPlayerCollider()
    {
        return playerCollider;
    }

    public void SetPlayerCollider (CapsuleCollider playerCollider)
    {
        this.playerCollider = playerCollider;
    }
}

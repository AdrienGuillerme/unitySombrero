using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherCapacityBehaviour : MonoBehaviour {

    public float speed = 5f;
    private string controllerName;
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

        if(activated) {
            capacityChosen.ActivateCapacity();

            if (capacityIntChosen != CapacityEnum.Freeze)
                Destroy(gameObject);
            else
                StartCoroutine(KillSelf(0.2f));
        }

        transform.position += speed * transform.forward * Time.deltaTime;
    }

    public void activate()
    {
        activated = true;
    }

    public void setParent(LaunchCapacity parent)
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
            Destroy(this.gameObject);
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

    IEnumerator KillSelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}

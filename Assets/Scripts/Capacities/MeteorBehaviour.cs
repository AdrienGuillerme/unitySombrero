using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {

    public int damages = 50;
    public float lifeTimeAfterActivation = 0.2f;
    public float lifeTime = 5f;
    public float forceDown = 5f;
    private bool asTouchedSomething;
    private string controllerName = "";
    private CapsuleCollider playerCollider;

    // Use this for initialization
    void Start () {
        asTouchedSomething = false;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0, -forceDown, 0), ForceMode.VelocityChange);
        StartCoroutine(KillSelf(lifeTime));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().getHurt(damages);
            setActivated();
        }

        if (other.gameObject.tag == "Ennemi")
        {
            other.gameObject.GetComponent<EnemyHealth>().GetHurt(damages, playerCollider);
            setActivated();
        }

        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "EnvironmentComponent")
            setActivated();
    }

    private void setActivated()
    {
        StartCoroutine(KillSelf(lifeTimeAfterActivation));
    }

    IEnumerator KillSelf(float time)
    {
        //yield return new WaitForSeconds(time);
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void SetControllerName(string controllerName)
    {
        this.controllerName = controllerName;
    }

    public void SetPlayerCollider(CapsuleCollider playerCollider)
    {
        this.playerCollider = playerCollider;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour {

    public int damages = 50;
    public float lifeTimeAfterActivation = 0.2f;
    private bool asTouchedSomething;

    // Use this for initialization
    void Start () {
        asTouchedSomething = false;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, -10, 0), ForceMode.VelocityChange);
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
            other.gameObject.GetComponent<EnemyHealth>().GetHurt(damages);
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
        yield return new WaitForSeconds(lifeTimeAfterActivation);
        Destroy(this.gameObject);
    }
}

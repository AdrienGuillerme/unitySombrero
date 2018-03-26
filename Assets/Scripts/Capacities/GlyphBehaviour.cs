using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphBehaviour : MonoBehaviour {

    public float lifeTime = 10f;
    public float lifeTimeAfterActivation = 1f;
    public int damages = 30;
    private bool asBeenActivated;
    private string controllerName = "";
    private CapsuleCollider playerCollider;

    void Start()
    {
        asBeenActivated = false;
        StartCoroutine(KillSelf(lifeTime));
    }


    IEnumerator KillSelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().getHurt(damages);
            setActivated();
        }

        if (other.gameObject.tag == "Ennemi")
        {
            other.gameObject.GetComponent<EnemyHealth>().GetHurt(damages, playerCollider);
            setActivated();
        }
    }

    private void setActivated()
    {
        if (!asBeenActivated)
        {
            StartCoroutine(KillSelf(lifeTimeAfterActivation));
            asBeenActivated = true;
        }
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

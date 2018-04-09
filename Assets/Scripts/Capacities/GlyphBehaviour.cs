using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphBehaviour : MonoBehaviour {

    public float lifeTime = 5f;
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

            ParticleSystem ps = GetComponent<ParticleSystem>();
            var col = ps.colorOverLifetime;
            col.enabled = true;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] {new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

            col.color = grad;

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

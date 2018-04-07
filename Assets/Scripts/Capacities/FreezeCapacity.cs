using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCapacity : MonoBehaviour, ICapacity {

    public AudioClip freezeSound;

    public float radius = 10f;
    public float freezeTime = 5f;
    private bool activated;
    private SphereCollider col;

	// Use this for initialization
	void Start () {
        activated = false;
        col = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(activated)
        {
            if (other.gameObject.tag.Equals("CharacterGroup"))
            {
                other.gameObject.GetComponentInChildren<CharacterMovement>().setSpeed(0f, freezeTime);
            }

            if (other.gameObject.tag == "Ennemi")
            {
                other.gameObject.GetComponentInParent<EnemyMove>().SetSpeed(0f, freezeTime);
            }
        }
    }

    public void ActivateCapacity()
    {
        if(!activated)
        {
            col.isTrigger = true;
            col.radius = radius;
            activated = true;
            GameObject obj = Resources.Load("FreezeEffect") as GameObject;
            AudioSource.PlayClipAtPoint(freezeSound, transform.position);
            Transform transformEffect = transform;
            transformEffect.Rotate(new Vector3(1, 0, 0), 90);
            transformEffect.Translate(Vector3.up*2);
            GameObject freezeEffect = Instantiate(obj, transformEffect.position, transformEffect.rotation);
        }
    }
}

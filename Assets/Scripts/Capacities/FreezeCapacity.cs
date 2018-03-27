using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCapacity : MonoBehaviour, ICapacity {

    private bool activated, triggered;
    private SphereCollider col;
	// Use this for initialization
	void Start () {
        activated = false;
        triggered = false;
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
                other.gameObject.GetComponentInChildren<CharacterMovement>().setSpeed(0f, 5f);
                triggered = true;
            }

            if (other.gameObject.tag == "Ennemi")
            {
                other.gameObject.GetComponentInParent<EnemyMove>().SetSpeed(0f, 5f);
                triggered = true;
            }
        }
    }

    public void ActivateCapacity()
    {
        if(!activated)
        {
            col.isTrigger = true;
            col.radius *= 10;
            activated = true;
            /*GameObject obj = Resources.Load("FreezeEffect") as GameObject;
            Transform transformEffect = transform;
            transformEffect.Rotate(new Vector3(1, 0, 0), 90);
            GameObject freezeEffect = Instantiate(obj, transform.position, transform.rotation);*/
        }
    }
}

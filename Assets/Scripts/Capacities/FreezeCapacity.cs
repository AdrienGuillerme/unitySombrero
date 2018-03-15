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

    void OnTriggerEnter(Collider other)
    {
        if(activated)
        {
            Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag.Equals("CharacterGroup") && !triggered)
            {
                other.gameObject.GetComponentInChildren<CharacterMovement>().setSpeed(0f, 5f);
                triggered = true;
            }

            /*if (other.gameObject.tag == "Ennemi")
            {
                other.gameObject.GetComponent<EnemyMove>();
                triggered = true;
            }*/
        }
    }

    public void ActivateCapacity()
    {
        if(!activated)
        {
            col.radius *= 10;
            activated = true;
        }
    }

    IEnumerator Freeze(float time)
    {
        // we freeze
        yield return new WaitForSeconds(time);
        // we unfreeze
    }
}

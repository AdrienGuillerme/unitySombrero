using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacityBehavior : MonoBehaviour {

    public string controllerName;

    // Use this for initialization
    void Start () {
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
    }

    void FixedUpdate()
    {
        // Store the input button.
        bool capacityIsTriggered = false;

        if (controllerName == "Keyboard")
        {
            
        }
        else
        {
            capacityIsTriggered = Input.GetButtonDown(controllerName + "Capacity");

            if (capacityIsTriggered)
            {
                // choose the capacity to trigger
                Debug.Log("Fiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiire");
                GameObject obj = Resources.Load("Glyph") as GameObject;
                Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
                Instantiate(obj, position, transform.rotation);
            }
        }

        // Animate the player.
        Animating(capacityIsTriggered);
    }

    void Animating(bool capacityIsTriggered)
    {
       
    }
}

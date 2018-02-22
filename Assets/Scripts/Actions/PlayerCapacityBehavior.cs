using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacityBehavior : MonoBehaviour{

    private string controllerName;
    public enum Capacity { Glyph, Repulsion };

    public Capacity capacityIntChosen = Capacity.Glyph;
    public Capacity capacityChosen;


    // Use this for initialization
    private void Start()
    {
        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
        switch (capacityIntChosen)
        {
            case Capacity.Glyph:
                capacityChosen = GetComponentInChildren<GlyphCapacity>();
                break;
            case Capacity.Repulsion:
                capacityChosen = GetComponentInChildren<RepulseCapacity>();
                break;
            default:
                Debug.Log("Default case");
                break;
        }
    }

    void Update()
    {
        // Store the input button.
        bool capacityIsTriggered = false;

        if (controllerName == "Keyboard")
        {
            
        }
        else
        {
            capacityIsTriggered = Input.GetButtonDown(controllerName + "ActivateCapacity");

            if (capacityIsTriggered)
            {
                capacityChosen.ActivateCapacity();
            }
        }

        // Animate the player.
        // Animating(capacityIsTriggered);
    }

    private void Repulsive()
    {
        Vector3 repulsiveDir;
        float repulsiveRadius = 3;
        float repulsivePower = 10;
        // float repulsiveDuration;
        // float cooldownTime;

    Collider[] colliders = Physics.OverlapSphere(transform.position, repulsiveRadius);
        int i = 0;
        while (i < colliders.Length)
        {
            GameObject target = colliders[i].gameObject;
            if (target.GetComponent<Rigidbody>() != null && target != gameObject)
            {
                Debug.Log("gameObject" + target);
                repulsiveDir = (target.transform.position - transform.position).normalized;
                target.transform.Translate(repulsiveDir * repulsivePower);

            }
            i++;
        }
    }

    private void Glyph()
    {
        GameObject obj = Resources.Load("Glyph") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
        Instantiate(obj, position, transform.rotation);
    }
}

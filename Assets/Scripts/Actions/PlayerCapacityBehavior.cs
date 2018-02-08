using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapacityBehavior : MonoBehaviour{

    private string controllerName;
    public enum Capacity { Glyph, Repulsion };
    public Capacity capacityChosen = Capacity.Glyph;

    // Use this for initialization
    private void Start()
    {
        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;

        //todo : définir quelle capacité depuis le parent
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
                switch (capacityChosen)
                {
                    case Capacity.Glyph:
                        Debug.Log("lancement du glyph");
                        Glyph();
                        break;
                    case Capacity.Repulsion:
                        Debug.Log("lancement de la repulsion");
                        Repulsive();
                        break;
                    default:
                        Debug.Log("Default case");
                        break;
                }
                // choose the capacity to trigger
                
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public string controllerName = "Joy1";
    public GameObject shield;

    Animator anim;
    PlayerMovement movement;

    private bool defending = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float lt = Input.GetAxisRaw(controllerName + "Stick3");
        if (defending)
        {
            if (lt < 0.9)
            {
                defending = false;
                movement.speed = 6;
            }
        }
        else if (lt > 0.9)
        {
            defending = true;
            movement.speed = 2;
        }
        Animating();
    }

    void Animating()
    {
        shield.SetActive(defending);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resurection : MonoBehaviour
{

    string controllerName;
    // public Slider resSlider;                                 // Reference to the UI's health bar.
    //public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    //Animator anim;                                              // Reference to the Animator component.
    //AudioSource playerAudio;                                    // Reference to the AudioSource component.

    PlayerHealth tmpHealth; // Reference to an other character health
    PlayerHealth myHealth;  // Reference to my own health function
    PlayerHealth characterHealth; // Reference to a health function to keep

    public double revSpeed;                                     //Divise le nombre de point a atteindre par le nombre rentré

    private bool couldRevive;
    private Collider col;

    private void Start()
    {
        //init gameController
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
        myHealth = GetComponentInParent<PlayerHealth>();

    }

    void Awake()
    {
        // Setting up the references.
        // anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider col)
    {
        if ((col.gameObject.tag == "Player") && (!myHealth.IsDead()))
        {
            tmpHealth = col.GetComponent<PlayerHealth>();
            if ((tmpHealth.IsDead()) && (!tmpHealth.IsRevived()))
            {
                couldRevive = true;
                characterHealth = tmpHealth;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            tmpHealth = col.GetComponent<PlayerHealth>();
            if (tmpHealth == characterHealth)
            {
                couldRevive = false;
                characterHealth = null;
            }
        }
    }


    void Update()
    {
        if (couldRevive)
        {
            if (Input.GetButton(controllerName + "Action"))
            {
                Revive();
            }
        }
    }

    void Revive()
    {
        characterHealth.getRevive();
        Debug.Log("I'm healing!");

    }
}


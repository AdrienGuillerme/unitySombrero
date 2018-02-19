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
    Transform trans;                                            //transform charactergroup
    Resurection resScript; // Reference to an other character rez script
    PlayerHealth myHealth;  // Reference to my own health function
    public PlayerHealth characterHealth; // Reference to a health function to keep

    public double revSpeed;                                     //Divise le nombre de point a atteindre par le nombre rentré


    private void Start()
    {
        //init gameController
        trans = this.GetComponentInParent<Transform>().parent.transform;
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
        myHealth =trans.Find("Player").GetComponent<PlayerHealth>();

    }

    void Awake()
    {
        // Setting up the references.
        // anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
    }
    /**/
    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            resScript = col.gameObject.GetComponentInParent<Transform>().parent.GetComponentInParent<Transform>().Find("ReviveCollider").GetComponentInChildren<Resurection>();
            resScript.SetHealth(myHealth);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            resScript = col.gameObject.GetComponentInParent<Transform>().parent.GetComponentInParent<Transform>().Find("ReviveCollider").GetComponentInChildren<Resurection>();
            resScript.RemoveHealth(myHealth);
        }
    }


    void Update()
    {
        if (characterHealth != null && !characterHealth.IsDead())
            this.RemoveHealth(characterHealth);

        if(!canRevive())
        {
            BoxCollider box = this.GetComponent<BoxCollider>();
            box.enabled = true;
        }
        else
        {
            BoxCollider box = this.GetComponent<BoxCollider>();
            box.enabled = false;
        }

        if (canRevive() && characterHealth != null && characterHealth.IsDead())
        {
            if (Input.GetButton(controllerName + "Action"))
            {
                Revive();
            }
        }
    }

    private bool canRevive()
    {
        return (!myHealth.IsDead());
    }

    void Revive()
    {
        characterHealth.getRevive();
        Debug.Log("I'm healing!");

    }

    void SetHealth(PlayerHealth health)
    {
        if (characterHealth == null)
            characterHealth = health;
    }

    void RemoveHealth(PlayerHealth health)
    {
        if (characterHealth == health)
            characterHealth = null;
    }
}


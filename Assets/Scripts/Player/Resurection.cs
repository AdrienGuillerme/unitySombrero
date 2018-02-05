using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resurection : MonoBehaviour {

    public string controllerName = "Joy1";
    public Slider resSlider;                                 // Reference to the UI's health bar.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    CharacterMovement playerMovement;                              // Reference to the player's movement.
    CharacterHealth characterHealth;                            // Reference to the health
    Resurection resurrection;
    
    public double revSpeed;                                     //Divise le nombre de point a atteindre par le nombre rentré
    
    private const int rezPoints = 1000;
    private int actualPoints;
    private bool isRevivable;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<CharacterMovement>();
        characterHealth = GetComponent<CharacterHealth>();


        actualPoints = 0;
    }

    void OnTriggerEnter(Collider col)
    {
        if (characterHealth.IsDead() && col.gameObject.tag == "Player" && characterHealth.IsDead())
        {
            isRevivable = true;
            col.GetComponentInParent<Resurection>().resurrection = this;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isRevivable = false;
            col.GetComponentInParent<Resurection>().resurrection = null;
        }
    }

    void Update()
    {
        //Animation de resurection si en cours
        //anim.SetTrigger("Res");
        if(!characterHealth.IsDead())
        {
            actualPoints = 0;
            resSlider.enabled = false;
        }
        else
        {
            if(actualPoints > 0)
            {
                resSlider.enabled = true;
            }
        }
        
        if(Input.GetButton(controllerName + "_Revive"))
        {
            Revive();
        }

        if(!resurrection.IsRevivable())
        {
            resurrection = null;
        }

    }

    void Revive()
    {
        if (resurrection != null && resurrection.IsRevivable())
        {
            Debug.Log("I'm healing!");
            resurrection.beRevived();
            StartCoroutine(WaitForRez(0.01f));
            
        }
    }
    IEnumerator WaitForRez(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void beRevived()
    {
        if(isRevivable)
        {
            if (actualPoints >= rezPoints)
            {
                characterHealth.Live();
                isRevivable = false;
            }
            else
            {
                actualPoints++;
                Debug.Log("Yay! Heal me!");
                resSlider.value = (actualPoints / rezPoints) * 100;
            }
        }
    }

    public bool IsRevivable() { return isRevivable; }
}
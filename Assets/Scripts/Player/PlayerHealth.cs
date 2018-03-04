using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    Rigidbody playerRigidbody;
    Vector3 knockback;

    //Previously in CharacterHealth
    public int maxHealth = 100;                                 // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    CharacterMovement characterMovement;                        // Reference to the player's movement.
    Attack characterAttack;                                     // Reference to the player's attack.
    Defense characterDefense;                                   // Reference to the player's defense.
    bool isDead;                                                // Whether the player is dead.
    bool isRevived;
    bool damaged;                                               // True when the player gets damaged.

    private int actualResPoints = 0;
    private int resPoints = 90;

    void Awake()
    {
        //Previously in CharacterHealth
        // Setting up the references.
        anim = GetComponentInParent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAttack = transform.parent.GetComponentInChildren<Attack>();
        characterDefense = transform.parent.GetComponentInChildren<Defense>();

        // Set the initial health of the player.
        currentHealth = maxHealth;

        playerRigidbody = GetComponentInParent<Rigidbody>();
    }

    //Previously in CharacterHealth
    void Update()
    {
        isRevived = false;
        /*
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        

        // Reset the damaged flag.
        damaged = false;*/
    }


    //Previously in CharacterHealth
    public void getHurt(int i)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= i;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
            Death();
    }

    //Previously in CharacterHealth
    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        Debug.Log("I'm Dead");
        // Tell the animator that the player is dead.
        anim.SetBool("death", true);

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off the movement and shooting scripts.
        characterMovement.enabled = false;
        characterAttack.enabled = false;
        characterDefense.enabled = false;
    }

    //Previously in CharacterHealth
    public void Live()
    {
        isDead = false;
        actualResPoints = 0;
        Debug.Log("Yay! I'm alive");
        anim.SetBool("death", false);
        characterMovement.enabled = true;
        characterAttack.enabled = true;
        characterDefense.enabled = true;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void getRevive()
    {
        isRevived = true;
        if (actualResPoints >= resPoints)
        {
            Live();
            isRevived = false;
        }
        else
        {
            actualResPoints++;
            Debug.Log("Yay! Heal me!");
            //resSlider.value = (actualPoints / rezPoints) * 100;
        }
    }
   
    //Previously in CharacterHealth
    public bool IsDead()
    {
        return this.isDead;
    }

    public bool IsRevived()
    {
        return this.isRevived;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyWeapons"  && isDead == false)
        {
            Debug.Log("You hurt me!!!");
            getHurt(10);
            knockback = col.transform.forward;
            KnockBack(knockback);
        } 
    }

    void KnockBack(Vector3 k)
    {
        k = k * 1000;
        playerRigidbody.AddForce(k);
    }
}

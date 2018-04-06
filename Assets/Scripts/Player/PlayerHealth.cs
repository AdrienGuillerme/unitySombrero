using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    Rigidbody playerRigidbody;
    Vector3 knockback;

	public AudioClip[] onHurt;
	public AudioClip onDie;
	public AudioClip onRevive;

    //Previously in CharacterHealth
    public int maxHealth = 100;                                 // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public GameObject healEffect = null;
    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    CharacterMovement characterMovement;                        // Reference to the player's movement.
    Attack characterAttack;                                     // Reference to the player's attack.
    Defense characterDefense;                                   // Reference to the player's defense.
    GameObject characterShield;
    bool isDead;                                                // Whether the player is dead.
    bool isRevived;
	bool isDamaged;                                             // True when the player gets damaged.

    private int actualResPoints = 0;
    private int resPoints = 90;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        characterMovement = GetComponent<CharacterMovement>();
        characterAttack = transform.parent.GetComponentInChildren<Attack>();
        characterDefense = transform.parent.GetComponentInChildren<Defense>();
        characterShield = characterDefense.transform.GetChild(0).gameObject;
        healEffect = this.transform.Find("HealEffect").gameObject;
        healEffect.SetActive(false);
        currentHealth = maxHealth;
        playerRigidbody = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        
        isRevived = false;
        if(!isDead && transform.position.y < -500)
        {
            Death();
        }
        /*
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
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
        isDamaged = true;

		int x = Random.Range(0, onHurt.Length);
		AudioSource.PlayClipAtPoint (onHurt [x], transform.position);

        // Reduce the current health by the damage amount.
        currentHealth -= i;
        healthSlider.value = currentHealth;
        //playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
            Death();
    }

	public void GetHeal(int i) {
		currentHealth += i;

		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
			isDamaged = false;
		}

		healthSlider.value = currentHealth;
	}

    //Previously in CharacterHealth

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        Debug.Log("I'm Dead");

		AudioSource.PlayClipAtPoint (onDie, transform.position);

        anim.SetBool("death", true);
        anim.SetBool("revive", false);
        this.GetComponentInParent<DontDestroy>().SubstractScore(100);
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        characterMovement.enabled = false;
        characterAttack.enabled = false;
        characterDefense.enabled = false;
        characterShield.SetActive(false);
    }

    public void ResetLife()
    {
        isDead = false;
        currentHealth = maxHealth;
        anim.SetBool("death", false);
        anim.SetBool("revive", true);
        actualResPoints = 0;
        Debug.Log("Yay! I'm alive");
        characterMovement.enabled = true;
        characterAttack.enabled = true;
        characterDefense.enabled = true;
        isDamaged = false;
        isRevived = false;
    }

    //Previously in CharacterHealth
    public void Live()
    {
        isDead = false;
        currentHealth = maxHealth;
        actualResPoints = 0;
        Debug.Log("Yay! I'm alive");
        anim.SetBool("revive", true);
        anim.SetBool("death", false);
        characterMovement.enabled = true;
        characterAttack.enabled = true;
        characterDefense.enabled = true;
		isDamaged = false;
        healthSlider.value = currentHealth;
    }

    public void getRevive()
    {
        isRevived = true;
        if (actualResPoints >= resPoints)
        {
            Live();
			AudioSource.PlayClipAtPoint (onRevive, transform.position);
            isRevived = false;
            
        }
        else
        {
            actualResPoints++;
            Debug.Log("Yay! Heal me!");
            healEffect.SetActive(true);
            StartCoroutine(EffectOff(0.3f));
            //ajouter animation de resurection

            //resSlider.value = (actualPoints / rezPoints) * 100;
        }
        this.GetComponentInParent<DontDestroy>().SubstractScore(1);
    }
   
    //Previously in CharacterHealth
    public bool IsDead()
    {
        return this.isDead;
    }

	public bool IsDamaged() {
		return this.isDamaged;
	}

    public bool IsRevived()
    {
        return this.isRevived;
    }

    IEnumerator EffectOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        healEffect.SetActive(false);
        //weaponTriggerL.SetActive(state);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyWeapons" && isDead == false)
        {
            {
                //Debug.Log("You hurt me!!!");
                getHurt(10);
                anim.SetBool("damaged", true);
                knockback = (col.GetComponentInParent<CapsuleCollider>().transform.position - transform.position);
                knockback.y = 0;
                knockback.Normalize();
                KnockBack(knockback);
            }
        }

        if (!col.transform.IsChildOf(this.transform) && col.tag == "Weapons" && col.GetComponentInParent<Attack>().isAttacking  && isDead == false)
        {
            //TODO: vérifier le friendly fire
            if(col.GetComponent<AttackTriggerCollision>().PosDiffFromStart() > 0.5f)
            {
                Debug.Log("Attacked by a mate");
                //getHurt(10);
                anim.SetBool("damaged", true);
                knockback = (col.GetComponentInParent<MeshCollider>().transform.position - transform.position);
                knockback.y = 0;
                knockback.Normalize();
                KnockBack(knockback);
            }
        } 
    }

    void KnockBack(Vector3 k)
    {
        k = k * -40000;
        playerRigidbody.AddForce(k);
    }
}
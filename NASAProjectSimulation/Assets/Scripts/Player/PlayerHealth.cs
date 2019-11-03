using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
   //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
   //public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    //AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    MouseLook mouseLookX;
    MouseLook mouseLookY;

    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
        // Setting up the references.
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        mouseLookX = GetComponent<MouseLook>();
        mouseLookX = this.GetComponentInChildren<MouseLook>();

        // Set the initial health of the player.
        GameInfo.currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            //damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        GameInfo.currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = GameInfo.currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (GameInfo.currentHealth <= 0 && !GameInfo.IsDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        //Debug.Log("Dead");
        // Set the death flag so this function won't be called again.
        GameInfo.IsDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        //anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        ///playerAudio.clip = deathClip;
        //.Play();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        mouseLookX.enabled = false;
        mouseLookX.enabled = false;
        Time.timeScale = 0f;
        GameInfo.IsDead = false;
        GameInfo.currentScore = 0;
        GameInfo.currentHealth = 100;
        GameInfo.GameIsPaused = false;
        GameInfo.LevelIteration = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        //playerShooting.enabled = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(2);
    }
}
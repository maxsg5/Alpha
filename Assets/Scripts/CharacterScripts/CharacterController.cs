/// <summary>
/// This class is used to control the character and handle collisions.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-10-23
/// Description: Initial Testing.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Weapon))]
public class CharacterController : MonoBehaviour
{
    #region Public Variables
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    public int extraJumpsValue; // The amount of extra jumps the character has.
    public AudioClip PistolShootSound; // The sound the character makes when it shoots the pistol.

    #endregion

    #region Private Variables
    private int extraJumps; // The number of extra jumps the character has.
    private bool isGrounded = false; // Whether or not the character is grounded.    
    private Weapon weapon; // The weapon the character is holding
    private Health health; // Reference to the health script.
    private CharacterMotor motor; // Reference to the character motor script.
    private AudioSource audioSource; // Reference to the audio source.
    #endregion


    /// <summary>
    /// Initialize character controller, health and weapon.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void Start()
    {
        extraJumps = extraJumpsValue; // Set the number of extra jumps the character has.
        //TODO: Get the weapon and health script.
        motor = GetComponent<CharacterMotor>();
        health = GetComponent<Health>(); // Get the health script
        weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
        audioSource = GetComponent<AudioSource>(); // Get the audio source.
        
    }


    /// <summary>
    /// FixedUpdate is used to handle character movement.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void FixedUpdate()
    {
        // Movement
        motor.Move(speed);

    }

    /// <summary>
    /// Update is used to handle character jumping and ground checking.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    void Update()
    {
        // Jumping
        // Check if the character is grounded.
         if(motor.IsGrounded())
        { 
            // Reset the extra jumps
            extraJumps = extraJumpsValue; 
        }
         //jumping
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) // If the space key is pressed and the character has extra jumps
        {
            motor.Jump(jumpForce); // Jump
            extraJumps--; // Decrease the number of extra jumps
        } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && motor.IsGrounded()) // If the space key is pressed and the character has no extra jumps and is grounded
        {
            motor.Jump(jumpForce); // Jump
        }
        //climbing ladders
        motor.LadderCheck();

        //shooting
        if (Input.GetMouseButtonDown(0)) // If the left mouse button is pressed
        {
            weapon.Fire(); // Shoot the weapon
            audioSource.PlayOneShot(PistolShootSound); // Play the pistol shoot sound.
        }
    }


    /// <summary>
    /// removes health from the character.
    /// </summary>
    /// <param name="damage">How much damage to deduct from health</param>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    public void Take_Damage(float damage){
        //call the health class to deal the damage to the character.
        Debug.Log("Taking Damage");
        health.Take_Damage(damage);
    }


    /// <summary>
    /// Checks for collisions
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        // check if character is hit by a bullet
        if(collision.gameObject.tag == "EnemyBullet"){
            Take_Damage(10);
        }
        if(collision.gameObject.tag == "Weapon"){
            Debug.Log("Weapon touched");
            collision.gameObject.transform.parent = this.transform;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Weapon_Single_Shot>().enabled = true;
            weapon = collision.gameObject.GetComponent<Weapon>();
        }
    }

}

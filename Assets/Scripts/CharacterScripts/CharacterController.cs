/// <summary>
/// This class is used to control the character and handle collisions.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-10-23
/// Description: Initial Testing.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Weapon))]
public class CharacterController : MonoBehaviour
{
	#region Delegates and Events
	public delegate void On_Weapon_Changed(Weapon new_weapon);
	public event On_Weapon_Changed Weapon_Changed;
	#endregion
	
    #region Public Variables
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    public int extraJumpsValue; // The amount of extra jumps the character has.
    public AudioClip PistolShootSound; // The sound the character makes when it shoots the pistol.

    #endregion

    #region Private Variables
    private int extraJumps; // The number of extra jumps the character has.
    private bool isGrounded = false; // Whether or not the character is grounded.    
    private Weapon active_weapon; // The weapon the character is holding
    private int active_weapon_i = 0; // Index of the active weapon in the weapons list
    private readonly List<Weapon> weapons = new List<Weapon>(); // Weapons currently in the character's possession.
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
    private void Start()
    {
        extraJumps = extraJumpsValue; // Set the number of extra jumps the character has.
        motor = GetComponent<CharacterMotor>();
        health = GetComponent<Health>(); // Get the health script
        this.active_weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
        this.weapons.Add(this.active_weapon);
        audioSource = GetComponent<AudioSource>(); // Get the audio source.
    }


    /// <summary>
    /// FixedUpdate is used to handle character movement.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    private void FixedUpdate()
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
    private void Update()
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
	        if (this.active_weapon != null) {
		        this.active_weapon.Fire(); // Shoot the weapon
            	audioSource.PlayOneShot(PistolShootSound); // Play the pistol shoot sound.
	        }
        }
        
        // Switch to next weapon
        if (Input.GetKeyDown(KeyCode.Q)) {
	        this.Set_Active_Weapon(this.active_weapon_i + 1);
        }

        //animations
        motor.HandleWalkAnimation();
        motor.HandleJumpAnimation();
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
	    // Note (Declan Simkins): Shouldn't need this; the damage script
	    // automatically damages anything with a health script so this
	    // will damage the character twice
	    // If we need the damage to be modulated by some character property
	    // we could add a health subclass that delegates some damage
	    // calculations to the character and then applies the damage
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

    /// <summary>
    /// Adds a new weapon the character's list of weapons
    /// </summary>
    /// <param name="weapon_obj">
    /// Pre-instantiated (i.e. not a prefab) weapon to be added to the
    /// character's list of weapons
    /// </param>
    public void Add_Weapon(GameObject weapon_obj)
    {
	    Weapon weapon = weapon_obj.GetComponent<Weapon>();
	    weapon_obj.transform.parent = this.transform;
	    
	    // Should be the point at which the weapon needs to be held
	    // by the character sprite which we can mark with an empty
	    // game object
	    weapon.transform.position = this.transform.position;
	    
	    this.weapons.Add(weapon);
	    if (this.weapons.Count == 1) {
		    this.Set_Active_Weapon(this.active_weapon_i);
	    }
    }

    public void Add_Ammo(GameObject ammo_pickup_obj)
    {
	    Ammo_Pickup ammo_pickup = ammo_pickup_obj.GetComponent<Ammo_Pickup>();
	    Weapon.Ammo ammo_type = ammo_pickup.Ammo_Type;
	    foreach (Weapon weapon in this.weapons) {
		    if (weapon.ammo_type == ammo_type) {
			    weapon.Add_Ammo(ammo_pickup.Ammo_Amount);
		    }
	    }
    }

    /// <summary>
    /// Switches the active weapon, treating the list of weapons as circular
    /// </summary>
    /// <param name="weapon_i">
    /// Index of the weapon to be set as the active weapons
    /// </param>
    private void Set_Active_Weapon(int weapon_i)
    {
	    // Change weapon sprite
	    weapon_i %= this.weapons.Count;
	    this.active_weapon = this.weapons[weapon_i];
	    this.active_weapon_i = weapon_i;
	    this.Weapon_Changed?.Invoke(this.active_weapon);
    }
}

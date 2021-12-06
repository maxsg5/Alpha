/// <summary>
/// This class is used to control the character and handle collisions.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-10-23
/// Description: Initial Testing.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// TODO: Flip the weapon sprite upside down when it rotates; flip the character based on weapon rotation
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
    public float jumpForce = 10f; // The force applied to the character when it jumps.
    public int extraJumpsValue; // The amount of extra jumps the character has.
    public AudioClip PistolShootSound; // The sound the character makes when it shoots the pistol.
    public bool disableMovement = false; // Whether or not the character can move.
    public PauseManager pauseManager; // The pause manager.
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
    private SpriteRenderer sprite_renderer;
    [SerializeField] private GameObject holster;
    [SerializeField] private GameObject pivot;
    #endregion

    #region Methods
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
        this.sprite_renderer = this.GetComponent<SpriteRenderer>();
        
        health = GetComponent<Health>(); // Get the health script
        this.health.Health_Changed += this.On_Health_Changed;
        
        audioSource = GetComponent<AudioSource>(); // Get the audio source.
    }

    private void On_Health_Changed(float new_health)
    {
	    // TODO: flash sprite red
    }

    /// <summary>
    /// FixedUpdate is used to handle character movement.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    private void FixedUpdate()
    {
        //if movement is disabled, return
        if(disableMovement)
        {
            motor.StopMoving();
            return;
        }
            
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
        //Check if the game is to be paused.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseManager.Pause();
        }

        //if movement is disabled, return
        if(disableMovement)
        {
            motor.StopMoving();
            return;
        }
            
        
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
	        }
        }
        
        // Switch to next weapon
        if (Input.GetKeyDown(KeyCode.Q)) {
	        this.Set_Active_Weapon(this.active_weapon_i + 1);
        }

        Vector3 pos = this.transform.position;
        bool aiming_left = Utilities.Mouse.Angle_To_Mouse(pos) > 90;
        aiming_left |= Utilities.Mouse.Angle_To_Mouse(pos) < -90;
        if (aiming_left) 
        {
	        this.motor.Face_Left();
        }
        else {
	        this.motor.Face_Right();
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
    /// Adds a new weapon the character's list of weapons
    /// </summary>
    /// <param name="weapon_obj">
    /// Pre-instantiated (i.e. not a prefab) weapon to be added to the
    /// character's list of weapons
    /// </param>
    public void Add_Weapon(GameObject weapon_obj)
    {
	    Weapon weapon = weapon_obj.GetComponent<Weapon>();
	    weapon_obj.transform.parent = this.pivot.transform;
	    
	    Vector3 local_scale = weapon_obj.transform.localScale;
	    weapon_obj.transform.localScale = new Vector3(
		    Mathf.Abs(local_scale.x)
		    , local_scale.y
		    , local_scale.z
		);
	    
	    weapon_obj.transform.rotation = this.pivot.transform.rotation;
	    weapon_obj.transform.position = this.holster.transform.position;

	    this.weapons.Add(weapon);
	    if (this.weapons.Count == 1) {
		    this.Set_Active_Weapon(this.active_weapon_i);
		    weapon_obj.SetActive(true);
	    }
	    else {
		    weapon_obj.SetActive(false);
		    weapon_obj.transform.rotation = this.active_weapon.transform.rotation;
		    weapon_obj.transform.position = this.active_weapon.transform.position;
	    }
    }

    public void Add_Ammo(GameObject ammo_pickup_obj)
    {
	    Ammo_Pickup ammo_pickup = ammo_pickup_obj.GetComponent<Ammo_Pickup>();
	    Weapon.Ammo ammo_type = ammo_pickup.Ammo_Type;
	    foreach (Weapon weapon in this.weapons) {
		    Debug.Log("Add " + ammo_pickup.Ammo_Amount + " ammo to: " + weapon.name);
		    if (weapon.ammo_type == ammo_type || ammo_type == Weapon.Ammo.ALL) {
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
	    if (this.active_weapon != null) {
		    this.active_weapon.gameObject.SetActive(false);
	    }

	    weapon_i %= this.weapons.Count;
	    this.active_weapon = this.weapons[weapon_i];
	    this.active_weapon_i = weapon_i;
	    this.active_weapon.gameObject.SetActive(true);

	    this.Weapon_Changed?.Invoke(this.active_weapon);
    }
	

    public void Enable_Movement()
    {
	    this.disableMovement = false;
    }

    #endregion
}

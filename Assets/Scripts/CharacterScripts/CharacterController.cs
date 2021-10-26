/// <summary>
/// This class is used to control the character.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-10-23
/// Description: Initial Testing.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LayerMask))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Health))]
public class CharacterController : MonoBehaviour
{

    public enum State
    {
        MOVING,
        IDLE,
        JUMPING, 
        SHOOTING,
        TAKE_DAMAGE,
        DEAD
    };

    #region Public Variables
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    public LayerMask whatIsGround; // The layer that is considered ground.
    public int extraJumpsValue; // The amount of extra jumps the character has.
    public State state; // The current state of the character.
    #endregion

    #region Private Variables
    private BoxCollider2D boxCollider;
    private Rigidbody2D physics; // Reference to the players rigidbody.
    private int extraJumps; // The number of extra jumps the character has.
    private bool isGrounded = false; // Whether or not the character is grounded.
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private bool isDead = false; // Whether or not the player is dead.
    private bool isShooting = false; // Whether or not the player is shooting.
    private bool isTakingDamage = false; // Whether or not the player is taking damage.
    private bool isInvincible = false; // Whether or not the player is invincible.
    private float move; // The direction the player is moving.
    private float jump; // The direction the player is jumping.
    private bool isJumping = false; // Whether or not the player is jumping.
    private Weapon weapon; // The weapon the character is holding
    private Health health; // Reference to the health script.
    private CharacterMotor motor; // Reference to the character motor script.
    #endregion


    /// <summary>
    /// Initialize extraJumps, physics, health and weapon.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void Start()
    {
        extraJumps = extraJumpsValue; // Set the number of extra jumps the character has.
        //TODO: Get the weapon and health script.
        health = GetComponent<Health>(); // Get the health script
        weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
        physics = GetComponent<Rigidbody2D>(); // Get the rigidbody
        boxCollider = GetComponent<BoxCollider2D>(); // get the boxCollider
        state = State.IDLE; // Set the state to idle.
        motor = GetComponent<CharacterMotor>(); // Get the character motor script.
    }
    /// <summary>
    /// Update is used for handling input and animating the player
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        //isShooting = Input.GetButtonDown("Shoot");
        jump = Input.GetAxis("Jump");

        //keep this here for now until I figure out how to implement this in the motor.
        // If the input is moving the player right and the player is facing left...
        if (physics.velocity.x > 0 && !facingRight)
        {
            // flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (physics.velocity.x < 0 && facingRight)
        {
            // flip the player.
            Flip();
        }
        
    }

    /// <summary>
    /// FixedUpdate is used when applying forces, torques, or other physics-related functions 
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void FixedUpdate()
    {
        switch(state){
            case State.MOVING:
                Move();
                break;
            case State.IDLE:
                Idle();
                break;
            case State.JUMPING:
                Jump();
                break;
            case State.SHOOTING:
                //Shoot();
                break;
            case State.TAKE_DAMAGE:
                //TakeDamage();
                break;
            case State.DEAD:
                //Dead();
                break;
        }
    }

    private void Idle()
    {
        if(move != 0){
            state = State.MOVING;
        }
        if(isShooting){
            state = State.SHOOTING;
        }
        if(jump != 0){
            state = State.JUMPING;
        }
    }

    private void Move()
    { 
        motor.Move(speed);
        if(move == 0){
            state = State.IDLE;
        };
    }

    private void Jump()
    {
        //Jumping
        // Check if the character is grounded.
        if(motor.IsGrounded())
        { 
            extraJumps = extraJumpsValue; // Reset the extra jumps
        }
         //jumping
        if (jump != 0 && extraJumps > 0) // If the space key is pressed and the character has extra jumps
        {
            motor.Jump(jumpForce); // Jump
            extraJumps--; // Decrease the number of extra jumps
        } else if(jump != 0 && extraJumps == 0 && !motor.IsGrounded()) // If the space key is pressed and the character has no extra jumps and is grounded
        {
            motor.Jump(jumpForce); // Jump
        }
        // if the character is not jumping and grounded switch state to IDLE.
        if(jump == 0 ){
            state = State.IDLE;
        };
    }

    /// <summary>
    /// Flips the character to face the other direction.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


   

    /// <summary>
    /// removes health from the character.
    /// </summary>
    /// <param name="damage">How much damage to deduct from health</param>
    public void Take_Damage(float damage){
        //call the health class to deal the damage to the character.
        health.Take_Damage(damage);
    }


}

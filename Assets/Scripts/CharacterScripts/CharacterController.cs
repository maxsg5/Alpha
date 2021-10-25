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
        MOVING_RIGHT,
        MOVING_LEFT,
        IDLE,
        IN_AIR, 
        SHOOTING,
        TAKE_DAMAGE,
        DEAD
    };

    #region Public Variables
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    public float checkRadius; // The radius of the ground check.
    public LayerMask whatIsGround; // The layer that is considered ground.
    public int extraJumpsValue; // The amount of extra jumps the character has.
    public State state; // The current state of the character.
    #endregion

    #region Private Variables
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d; // Reference to the players rigidbody.
    private int extraJumps; // The number of extra jumps the character has.
    private bool isGrounded = false; // Whether or not the character is grounded.
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private bool isDead = false; // Whether or not the player is dead.
    private bool isShooting = false; // Whether or not the player is shooting.
    private bool isTakingDamage = false; // Whether or not the player is taking damage.
    private bool isInvincible = false; // Whether or not the player is invincible.
    private bool isMovingRight = false; // Whether or not the player is moving right.
    private bool isMovingLeft = false; // Whether or not the player is moving left.
    private bool isJumping = false; // Whether or not the player is jumping.
    private bool isInAir = false; // Whether or not the player is jumping.
    private Weapon weapon; // The weapon the character is holding
    private Health health; // Reference to the health script.
    #endregion


    /// <summary>
    /// Initialize extraJumps, rb2d, health and weapon.
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
        rb2d = GetComponent<Rigidbody2D>(); // Get the rigidbody
        boxCollider = GetComponent<BoxCollider2D>(); // get the boxCollider
        state = State.IDLE; // Set the state to idle.
    }
    /// <summary>
    /// Update is used for handling input and animating the player
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void Update()
    {
        isMovingRight = Input.GetButtonDown("MoveRight");
        isMovingLeft = Input.GetButtonDown("MoveLeft");
        //isShooting = Input.GetButtonDown("Shoot");
        isJumping = Input.GetButtonDown("Jump");
        
        // If the input is moving the player right and the player is facing left...
        if (rb2d.velocity.x > 0 && !facingRight)
        {
            // flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (rb2d.velocity.x < 0 && facingRight)
        {
            // flip the player.
            Flip();
        }
        
        #region old code
        // //Jumping
        // // Check if the character is grounded.
        // if(IsGrounded())
        // { 
        //     extraJumps = extraJumpsValue; // Reset the extra jumps
        // }
        //  //jumping
        // if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) // If the space key is pressed and the character has extra jumps
        // {
        //     rb2d.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
        //     extraJumps--; // Decrease the number of extra jumps
        // } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded) // If the space key is pressed and the character has no extra jumps and is grounded
        // {
        //     rb2d.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
        // }
        #endregion
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
            case State.MOVING_RIGHT:
                MoveRight();
                break;
            case State.MOVING_LEFT:
                MoveLeft();
                break;
            case State.IDLE:
                Idle();
                break;
            case State.IN_AIR:
                //InAir();
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
        #region old code

        // // movement
        // float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        // Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        // rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);  // Set the velocity of the rigidbody to the velocity created above

        // // If the input is moving the player right and the player is facing left...
        // if (x > 0 && !facingRight)
        // {
        //     // flip the player.
        //     Flip();
        // }
        // // Otherwise if the input is moving the player left and the player is facing right...
        // else if (x < 0 && facingRight)
        // {
        //     // flip the player.
        //     Flip();
        // }
        #endregion
    }

    private void Idle()
    {
        if(isMovingRight){
            state = State.MOVING_RIGHT;
        }
        if(isMovingLeft){
            state = State.MOVING_LEFT;
        }
        if(isShooting){
            state = State.SHOOTING;
        }
        if(isJumping){
            state = State.IN_AIR;
        }
    }

    private void MoveRight()
    { 
        float x = Input.GetAxis("MoveRight"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);  // Set the velocity of the rigidbody to the velocity created above

    }
    private void MoveLeft()
    {
        float x = Input.GetAxis("MoveLeft"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);  // Set the velocity of the rigidbody to the velocity created above

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
    /// bool that determines if the character is grounded or not
    /// uses a BoxCast to cast a box under the player
    /// </summary>
    /// <returns>True if the character is grounded</returns>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    private bool IsGrounded(){
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeight), Vector2.right * (boxCollider.bounds.extents.x), rayColor);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
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

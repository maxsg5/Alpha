/// <summary>
/// This class is used to control the character.
/// </summary>
/// Author: Max Schafer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{

    
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    public Transform groundCheck; // The position of the ground check.
    public float checkRadius; // The radius of the ground check.
    public LayerMask whatIsGround; // The layer that is considered ground.
    public int extraJumpsValue; // The amount of extra jumps the character has.


    private Rigidbody2D rb2d; // Reference to the players rigidbody.
    private int extraJumps; // The number of extra jumps the character has.
    private bool isGrounded = false; // Whether or not the character is grounded.
    private bool facingRight = true;  // For determining which way the player is currently facing.
    //private Weapon weapon; // The weapon the character is holding
    //private Health health; // Reference to the health script.


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue; // Set the number of extra jumps the character has.
        //TODO: Get the weapon and health script.
        //health = GetComponent<Health>(); // Get the health script
        //weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
        rb2d = GetComponent<Rigidbody2D>(); // Get the rigidbody
    }


    // FixedUpdate should be used when applying forces, torques, or other physics-related functions 
    void FixedUpdate()
    {
        // Check if the character is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); 

        //movement
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        rb2d.velocity = new Vector2(x * speed, rb2d.velocity.y);  // Set the velocity of the rigidbody to the velocity created above

        // If the input is moving the player right and the player is facing left...
        if (x > 0 && !facingRight)
        {
            // flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (x < 0 && facingRight)
        {
            // flip the player.
            Flip();
        }

    }
    // Update is called once per frame
    void Update()
    {
        //Jumping
        if(isGrounded)
        { 
            extraJumps = extraJumpsValue; // Reset the extra jumps
        }
         //jumping
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) // If the space key is pressed and the character has extra jumps
        {
            rb2d.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
            extraJumps--; // Decrease the number of extra jumps
        } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded) // If the space key is pressed and the character has no extra jumps and is grounded
        {
            rb2d.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
        }
        
    }


    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

   

    


   


}

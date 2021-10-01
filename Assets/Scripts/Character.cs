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

    //private Weapon weapon; // The weapon the character is holding
    //private Health health; // Reference to the health script.
    public float speed = 10f; // The speed the character moves at.
    public float jumpForce = 5f; // The force applied to the character when it jumps.
    private Rigidbody2D rb2d; // Reference to the players rigidbody.
    private bool isGrounded = false; // Whether or not the character is grounded.
    private bool canDoubleJump = false; // Whether or not the character can double jump.

    // Start is called before the first frame update
    void Start()
    {
        //health = GetComponent<Health>(); // Get the health script
        //weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
        rb2d = GetComponent<Rigidbody2D>(); // Get the rigidbody
    }

    // Update is called once per frame
    void Update()
    {

        //movement
        HandleMovement();
        Debug.Log(isGrounded);


        
    }

    /// <summary>
    /// Handles the movement of the character.
    /// </summary>
    /// Author: Max Schafer
    private void HandleMovement(){
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        rb2d.velocity = velocity * speed; // Set the velocity of the rigidbody to the velocity created above

        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                
                Jump();
                canDoubleJump = true;

            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

    }

    /// <summary>
    /// Handles the jumping of the character.
    /// </summary>
    /// Author: Max Schafer
    private void Jump(){
        rb2d.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
        
    }


    /// <summary>
    /// Checks for collisions.
    /// </summary>
    /// Author: Max Schafer
    void OnCollisionEnter2D(Collision2D col)
    {
        //check if the player is on the ground
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }


}

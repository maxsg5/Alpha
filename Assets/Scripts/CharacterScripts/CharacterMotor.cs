using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The character motor class is used to control the movement and physics of the character.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-11-12
/// Description: Initial Testing.
public class CharacterMotor : MonoBehaviour
{
    public LayerMask whatIsGround; // The layer that is considered ground.
    public LayerMask whatIsLadder; // The layer that is considered ladder.
    private Rigidbody2D physics; // The rigidbody of the character.
    private float boxHeight; // height of the boxCast for ground detection.
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private BoxCollider2D boxCollider; // The boxCollider of the character.
    private bool isClimbingLadder = false; // Is the character currently climbing a ladder?
    private Animator animator; // The animator of the character.
    

    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxHeight = boxCollider.size.y;
        animator = GetComponent<Animator>();
    }
    
    /// <summary>
    /// bool that determines if the character is grounded or not
    /// uses a BoxCast to cast a box under the player
    /// </summary>
    /// <returns>True if the character is grounded</returns>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
     public bool IsGrounded(){
        float extraHeight = .5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);
        // draw the ray in the scene view for debugging purposes.
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeight), Vector2.right * (boxCollider.bounds.extents.x), rayColor);
        return raycastHit.collider != null;
    }

    public void LadderCheck(){
        RaycastHit2D ladderCast = Physics2D.Raycast(transform.position, Vector2.up, 5f, whatIsLadder);
        Debug.DrawRay(transform.position, Vector2.up * 5f, Color.green);
        if(ladderCast.collider != null){
            if(Input.GetKeyDown(KeyCode.W)){
                isClimbingLadder = true;
            }
        }else{
            isClimbingLadder = false;
            physics.gravityScale = 1;
        }
        if(isClimbingLadder){
            Climb();
        }
    }


    private void Climb(){
        float inputVerticle = Input.GetAxisRaw("Vertical");
        physics.velocity = new Vector2(physics.velocity.x, inputVerticle * 5f);
        physics.gravityScale = 0f;
    }

    /// <summary>
    /// Adds a force to the character in the up direction
    /// </summary>
    /// <param name="jumpForce">force of the jump</param>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    public void Jump (float jumpForce)
    {
         physics.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
    }

    /// <summary>
    /// Handles the side to side movement of the character
    /// </summary>
    /// <param name="speed">speed of the movement</param>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    public void Move(float speed)
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        physics.velocity = new Vector2(x * speed, physics.velocity.y);  // Set the velocity of the rigidbody to the velocity created above
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
    /// Handles the switching between walking and idle animations based on character velocity.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-25
    /// Description: Initial Testing.
    public void HandleWalkAnimation(){
        if(physics.velocity.x == 0){
            animator.SetBool("walking", false);
            animator.SetBool("idle", true);
        }
        if(physics.velocity.x != 0){
            animator.SetBool("walking", true);
            animator.SetBool("idle", false);
        }
    }
}

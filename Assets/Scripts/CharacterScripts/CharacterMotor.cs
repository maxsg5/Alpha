using System;
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
	[SerializeField] private float acceleration_rate = 1;
	[SerializeField] private float deceleration_rate = 0.5f;
	[SerializeField] private float max_speed = 5;
	[SerializeField] private float aerial_max_acceleration = 0.25f;
	
    #region Public Variables
    public LayerMask whatIsGround; // The layer that is considered ground.
    public LayerMask whatIsLadder; // The layer that is considered ladder.
    #endregion

    #region Private Variables
    private Rigidbody2D physics; // The rigidbody of the character.
    private float boxHeight; // height of the boxCast for ground detection.
    private bool facingRight = true;  // For determining which way the player is currently facing.
    private BoxCollider2D boxCollider; // The boxCollider of the character.
    private bool isClimbingLadder = false; // Is the character currently climbing a ladder?
    private Animator animator; // The animator of the character.
    private SpriteRenderer spriteRenderer; // The spriteRenderer of the character.

    private bool grounded;
    private Vector2 acceleration;

    #endregion

    #region Methods
    /// <summary>
    /// Initialize the reference variables
    /// set the boxHeight to hight of the boxCollider
    /// set up the intial animation state.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    void Start()
    {
	    spriteRenderer = this.GetComponent<SpriteRenderer>();
	    physics = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxHeight = boxCollider.size.y;
        animator = GetComponent<Animator>();
        animator.SetBool("grounded", true);
        animator.SetBool("jumping", false);
    }

    private void FixedUpdate()
    {
	    if (!this.grounded) {
		    this.acceleration.x = Mathf.Clamp(this.acceleration.x, -this.aerial_max_acceleration, this.aerial_max_acceleration);
	    }
	    this.physics.velocity += this.acceleration;
	    
	    bool moving_right = this.physics.velocity.x >= 0;
	    Vector2 deceleration_vec = new Vector2(moving_right ? -this.deceleration_rate : this.deceleration_rate, 0);

	    if (this.grounded) {
		    this.physics.velocity += deceleration_vec;
	    }

	    if (moving_right && this.physics.velocity.x < 0
	        || !moving_right && this.physics.velocity.x > 0) {
		    this.physics.velocity = new Vector2(0, this.physics.velocity.y);
	    }

	    if (moving_right && this.physics.velocity.x > this.max_speed) {
		    this.physics.velocity = new Vector2(this.max_speed, this.physics.velocity.y);
	    }
	    else if (!moving_right && this.physics.velocity.x < -this.max_speed) {
		    this.physics.velocity = new Vector2(-this.max_speed, this.physics.velocity.y);
	    }
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
        float extraHeight = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);
        // draw the ray in the scene view for debugging purposes.
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        /*Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extraHeight), Vector2.right * (boxCollider.bounds.extents.x), rayColor);*/
        animator.SetBool("grounded", raycastHit.collider != null);
        
        return raycastHit.collider != null;
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
	    if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
		    this.grounded = true;
	    }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
	    if (other.gameObject.layer ==  LayerMask.NameToLayer("Ground")) {
		    this.grounded = false;
	    }
    }

    /// <summary>
    /// Method to determine if the character can start climbing a ladder or not.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    public void LadderCheck(){
        //Cast a ray upwards to see if there is a ladder.
        RaycastHit2D ladderCast = Physics2D.Raycast(transform.position, Vector2.up, 5f, whatIsLadder);
        Debug.DrawRay(transform.position, Vector2.up * 5f, Color.green);
        //If there is a ladder, allow player to start climbing by pressing W and. set the animator to climbing.
        if(ladderCast.collider != null){
            if(Input.GetKeyDown(KeyCode.W)){
                isClimbingLadder = true;
            }
        }else{
            isClimbingLadder = false;
            physics.gravityScale = 1;
            animator.SetBool("climbing", false);
        }
        if(isClimbingLadder){
            Climb();
            animator.SetBool("climbing", true);
            animator.SetBool("grounded", false);
            animator.SetBool("jumping", false);
            animator.SetBool("walking", false);
        }
    }

    /// <summary>
    /// Called when player is climbing the ladder
    /// moves the player up and down the ladder using Vertial input.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
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
        Vector2 velocity = new Vector2(x * this.acceleration_rate, 0); // Create a new vector2 with the x value of the horizontal input
        //physics.velocity = new Vector2(Mathf.Clamp(this.physics.velocity.x + (x * speed), -speed, speed), physics.velocity.y);  // Set the velocity of the rigidbody to the velocity created above
        this.acceleration = velocity;

        /*// If the input is moving the player right and the player is facing left...
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
        }*/
    }

    /// <summary>
    /// Stops the movement of the character
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    /// Description: Initial Testing.
    public void StopMoving(){
        physics.velocity = new Vector2(0, physics.velocity.y);
        animator.SetBool("walking", false);
        animator.SetBool("jumping", false);
        animator.SetBool("grounded", true);
        animator.SetBool("idle", true);
    }

    /// <summary>
    /// Flips the character to face the other direction.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    /// Description: Initial Testing.
    public void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		this.spriteRenderer.flipX = !this.facingRight;
		
		/*// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;*/
	}

    /// <summary>
    /// Flips the character to face left
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    public void Face_Left()
    {
	    this.facingRight = false;
	    this.spriteRenderer.flipX = !this.facingRight;
    }

    /// <summary>
    /// Flips the character to face right
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-10-23
    public void Face_Right()
    {
	    this.facingRight = true;
	    this.spriteRenderer.flipX = !this.facingRight;
    }

    /// <summary>
    /// Handles the jump animation based on the grounded state of the character.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-12
    public void HandleJumpAnimation(){
        if(IsGrounded()){
            animator.SetBool("jumping", false);
        }else{
            animator.SetBool("jumping", true);
        }
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

    #endregion
}

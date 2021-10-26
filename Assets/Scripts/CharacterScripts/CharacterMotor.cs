using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public LayerMask whatIsGround;
    private const float CLOSE_SPEED = 0.01f;
    private const float CLOSE_SPEED2 = CLOSE_SPEED * CLOSE_SPEED;
    private Rigidbody2D physics;
    //private Vector3 groundNormal = Vector3.up;
    private Transform body;
    //private Vector3 bodyCenter;
    private float boxHeight;
    private Vector2 capsuleCenter;

    private BoxCollider2D boxCollider;
    

    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        body = transform.Find("Body");
        boxCollider = GetComponent<BoxCollider2D>();
        boxHeight = boxCollider.size.y;
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
        float extraHeight = 1f;
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
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public void Jump (float jumpForce)
    {
         physics.velocity = Vector2.up * jumpForce; // Add a force to the rigidbody in the up direction
    }

    public void Move(float speed)
    {
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        physics.velocity = new Vector2(x * speed, physics.velocity.y);  // Set the velocity of the rigidbody to the velocity created above
    }
}

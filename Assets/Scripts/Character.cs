/// <summary>
/// This class is used to control the character.
/// </summary>
/// Author: Max Schafer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{

    //private Weapon weapon; // The weapon the character is holding
    //private Health health; // Reference to the health script.
    public float speed = 10f; // The speed the character moves at.
    private Rigidbody2D rb2d; // Reference to the players rigidbody.


    // Start is called before the first frame update
    void Start()
    {
        //health = GetComponent<Health>(); // Get the health script
        //weapon = GetComponentInChildren<Weapon>(); // Get the weapon script from the child object
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //movement
        HandleMovement();

        
    }

    private void HandleMovement(){
        float x = Input.GetAxis("Horizontal"); // Get the horizontal input
        Vector2 velocity = new Vector2(x, 0); // Create a new vector2 with the x value of the horizontal input
        rb2d.velocity = velocity * speed; // Set the velocity of the rigidbody to the velocity created above
    }
}

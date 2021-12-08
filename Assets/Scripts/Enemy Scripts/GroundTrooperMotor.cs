using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

/// <summary>
/// Motor script for the ground trooper enemy. Controls the movement of the enemy.
/// </summary>
/// 
/// Author: Josh Coss (JC)
/// 
/// Variables:
/// rigidbody       Rigidbody2D of the enemy
/// movement        PathMove script of the enemy
/// weapon          Weapon script of the enemy
/// animator        Animator controller
/// deathEffect     Particle effect to play when the enemy dies
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GroundTrooperMotor : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PathMove movement;
    private Weapon weapon;
    private Animator animator;
    public GameObject deathEffect;

    /// <summary>
    /// Gets the movement, rigidbody, weapon, and animator components of the enemy.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    void Awake() {
        movement = GetComponent<PathMove>();
        rigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon_Single_Shot>();
        animator = GetComponent<Animator>();
        
    }

    /// <summary>
    /// Moves the enemy forward using the PathMove script, and tells the animator
    /// to play the walking animation.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void MoveForward() {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
        
    }

    /// <summary>
    /// Fires the enemy's weapon and stops the enemy from moving forward.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Attack()
    {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 0.0f);
        weapon.Fire();
    }

    /// <summary>
    /// Plays the hurt animation and stops the enemy from moving forward.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void GetHurt() {
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("hurt", true);
    }

    /// <summary>
    /// Plays the death effect and destroys the enemy
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Death() {
        Instantiate(deathEffect, transform.position + new Vector3(0.0f, 0.25f, 0.0f), transform.rotation);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Knocks the enemy back based on the direction of the hit.
    /// </summary>
    /// <param name="force">Vector2</param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void knockback(Vector2 force) {
        rigidbody.AddForce(new Vector2(0.3f * force.x, 2.0f), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Sets the movement speed of the enemy
    /// </summary>
    /// <param name="speed">float</param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void setSpeed(float speed) {
        movement.setMoveSpeed(speed);
    }
    
    /// <summary>
    /// Called when the enemy collides with another object. Sets the force (direction) of
    /// the hit and knocks the enemey back if the collision object is a projectile.
    /// </summary>
    /// <param name="collision"></param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Vector2 force = collision.attachedRigidbody.velocity;
            knockback(force);
        }
    }
}

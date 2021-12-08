using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Motor script for the martian enemy. Controls the movement and animation
/// of the martion.
/// </summary>
/// 
/// Author: Josh Coss (JC)
/// 
/// Variables:
/// DEATH_ANIMATION_TIME         The time it takes for the death animation to play
/// death_start_time             The time the death animation started
/// rigidbody                    The rigidbody2D of the martian
/// movement                     the PathMove script attached to the martian
/// melee                        The Damage script attached to the martian
/// animator                     The animator attached to the martian
/// deathEffect                  The particle effect to play when the martian dies
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BasicMartianMotor : MonoBehaviour
{
    private float DEATH_ANIMATION_TIME = (85.0f / 60.0f);
    private float death_start_time;

    private Rigidbody2D rigidbody;
    private PathMove movement;
    private Damage melee;
    private Animator animator;
    public GameObject deathEffect;

    /// <summary>
    /// Gets the movement, rigidbody, melee, and animator components of the enemy.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    void Awake() {
        movement = GetComponent<PathMove>();
        rigidbody = GetComponent<Rigidbody2D>();
        melee = transform.Find("Melee").GetComponent<Damage>();
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("dead", false);
    }

    /// <summary>
    /// Sets animator parameters to walk and calls the movement script to start moving.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void MoveForward()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 2.9f);
        movement.Move(rigidbody);
    }

    /// <summary>
    /// Sets the animator parameters to run and calls the movement script to start moving.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void RunForward() {
        animator.SetBool("attack", false);
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.1f);
        movement.Move(rigidbody);
    }
    
    /// <summary>
    /// Sets the animator parameters to attack and calls the melee script to start attacking the
    /// player collider
    /// </summary>
    /// <param name="player_collider">Target's collider</param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Attack(Collider2D player_collider)
    {
        animator.SetBool("attack", true);
        animator.SetFloat("speed", 0.0f);
        melee.doDamage(player_collider);

    }

    /// <summary>
    /// Sets the animator parameters to get hurt
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void GetHurt()
    {
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("hurt", true);
    }

    /// <summary>
    /// Knocks the enemy back based on the direction of the force
    /// </summary>
    /// <param name="force">Vector2</param>
    public void knockback(Vector2 force) {
        rigidbody.AddForce(new Vector2(0.3f * force.x, 2.0f), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Plays the death effect and destroys the enemy
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Death()
    {
        Instantiate(deathEffect, transform.position + new Vector3(0.0f, -0.4f, 0.0f), transform.rotation);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Returns if dying is still in progress
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Dying() {        
        if (this.DyingInProgress()) {
            return;
        } 
    }

    /// <summary>
    /// Sets the death_start_time
    /// </summary>
    /// <param name="time"></param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void setDeathTime(float time) {
        animator.SetBool("dead", true);
        animator.SetFloat("speed", 0.0f);
        death_start_time = time;
    }

    /// <summary>
    /// Returns whether or not the death animation is still playing
    /// </summary>
    /// <returns></returns>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public bool DyingInProgress() {
        return Time.time - death_start_time < DEATH_ANIMATION_TIME;

    }

    /// <summary>
    /// returns whether the enemy is dead or not
    /// </summary>
    /// <returns></returns>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public bool Dead() {
        if (Time.time - death_start_time < DEATH_ANIMATION_TIME + 1.0f)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Sets the movement speed of the enemy
    /// </summary>
    /// <param name="speed"></param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void setMoveSpeed(float speed) {
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

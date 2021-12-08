using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

/// <summary>
/// Motor script for the ground trooper enemy. Controls the movement of the enemy.
/// 
/// **CURRENTLY INCOMPLETE**
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
public class BomberMotor : MonoBehaviour
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
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Calls the movement script to move the enemy.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void MoveForward()
    {
        movement.Move(rigidbody);
    }

    /// <summary>
    /// Calls the weapon script to fire the enemy's weapon. 
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public void Attack()
    {
        weapon.Fire();
    }

}

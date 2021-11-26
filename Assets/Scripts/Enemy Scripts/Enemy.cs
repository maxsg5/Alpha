using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy parent abstract class
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// VARIABLES:
/// STRENGTH
/// health          Health object
/// movement        PathMove object
/// target          Enemy's target
/// sensor          Sensor object
/// physics         Rigidbody2d object
public abstract class Enemy 
{
    public Health health;
    //public ArmorClass armor;
    public Transform transform;
    public Rigidbody2D rigidbody;
    public Weapon weapon;         
    public PathMove movement;
    public Animator animator;


    public Enemy(Transform transform, Rigidbody2D rigidbody, Weapon weapon, PathMove movement,
            Health health, Animator animator){
        this.transform = transform;
        this.rigidbody = rigidbody;
        this.weapon = weapon;
        this.movement = movement;
        this.health = health;
        this.animator = animator;
    }

    public abstract void MoveForward();
    public abstract void Attack();
    public abstract void Idle();
}

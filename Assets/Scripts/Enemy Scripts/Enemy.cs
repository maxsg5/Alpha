using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy parent class
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// VARIABLES:
/// STRENGTH
/// movement        PathMove object
/// target          Enemy's target
/// sensor          Sensor object
/// physics         Rigidbody2d object
public abstract class Enemy : MonoBehaviour
{
    public const float STRENGTH = 0.0f;

    //public WeaponClass weapon;         
    //public HealthClass health;
    //public ArmorClass armor;
    
    public PathMove movement;
    public Transform target;
    public Sensor sensor; 

    /// Date        Author      Description
    /// 2021-10-14  JC          Changed Rigidbody to Rigidbody2D
    public Rigidbody2D physics;

}

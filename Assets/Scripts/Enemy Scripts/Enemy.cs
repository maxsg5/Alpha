using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public const float STRENGTH = 0.0f;
    public const float U_SPEED = 0.5f;

    //public WeaponClass weapon;         
    //public HealthClass health;
    //public ArmorClass armor;
    
    public PathMove movement;
    public Transform target;
    public Sensor sensor; 
    public Rigidbody physics;

}

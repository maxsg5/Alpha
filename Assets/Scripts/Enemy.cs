using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public const float STRENGTH = 0.0f;
    public const float U_SPEED = 0.5f;

    //public WeaponClass weapon;
    //public SensorClass sensor;           
    //public HealthClass health;
    //public ArmorClass armor;
    
    public Transform[] cPoints;
    public Interpolator interpolator;
    public Rigidbody physics;

    public float u;
    public float speed;
    public bool closedLoop = false;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SensorSystem;

/// <summary>
/// The bomber class flies in the air and drops bombs on the level if it sees
/// the player
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// Variables
/// moveSpeed       Speed at which the enemy moves
public class Bomber : Enemy
{
    public const float moveSpeed = 8.0f;

    /// <summary>
    /// Gets the movement, sensor, and rigidbody components of the enemy, as
    /// well as sets the turn and move speed
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Changed Rigidbody to Rigidbody2D
    void Start()
    {
        movement = GetComponent<PathMove>();
        //This class uses a sphere sensor
        sensor = transform.Find("Sensor").GetComponent<SNSSphere>();
        physics = GetComponent<Rigidbody2D>();

        movement.setMoveSpeed(moveSpeed);
    }

    /// <summary>
    /// Prints a test message to the console if the sensor can see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    void Update()
    {
        switch (state) {
            case STATE.Move:
                if (sensor.CanSee(target)) {
                    Debug.Log("I'm Flying");
                    //state = STATE.Attack;
                }
                break;
            case STATE.Attack:
                //attack();
                break;
            case STATE.Hurt:
                break;
            case STATE.Dying:
                break;
            case STATE.Dead:
                break;
        }
    }
}

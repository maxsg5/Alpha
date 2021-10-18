using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SensorSystem;

/// <summary>
/// The human Ground Trooper enemy class. Walks along the ground and shoots
/// at the player if it can see them
/// </summary>
/// 
/// Author: Josh Coss       (JC)
/// 
/// Variables
/// moveSpeed       Speed at which the enemy moves
public class GroundTrooper : Enemy
{
    public const float moveSpeed = 4.0f;

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
        // This class uses a sector sensor
        sensor = transform.Find("Sensor").GetComponent<SNSSector>();
        physics = GetComponent<Rigidbody2D>();

        movement.setMoveSpeed(moveSpeed);

        state = STATE.Move;
    }

    /// <summary>
    /// Prints a test message to the console if the sensor can see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    void FixedUpdate()
    {
        switch (state) {
            case STATE.Move:
                if (sensor.CanSee(target)) {
                    Debug.Log("Hello Sphere");
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

    void attack() {
        movement.stop();
    }
}

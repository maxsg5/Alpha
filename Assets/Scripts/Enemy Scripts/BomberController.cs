using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SensorSystem;

/// <summary>
/// The bomber class flies in the air and drops bombs on the level if it sees
/// the player
/// 
/// ***CURRENTLY INCOMPLETE***
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// Variables
/// STATE           enum of enemy states
/// sensor          _SNSSensor attached to this object
/// target          The object that the enemy is currently targeting
/// moveSpeed       Speed at which the enemy moves
/// motor           GroundTrooperMotor attached to this object
/// state           The current state of the enemy
/// health          The health of the enemy
/// prevHealth      The previous health of the enemy before it was damaged
public class BomberController : MonoBehaviour
{
    public enum STATE {
        Move,
        Attack,
        Hurt,
        Dying,
        Dead
    };

    private _SNSSensor sensor;
    public Transform target;
    public float moveSpeed = 4.0f;

    private BomberMotor motor;
    private STATE state;
    private Health health;

    private float prevHealth;

    /// <summary>
    /// Gets the sensor and health components of the enemy, as
    /// well as sets the prevHealth, initial state, and move speed.
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Changed Rigidbody to Rigidbody2D
    /// 2021-12-08  JC          Added motor and state variables
    void Start()
    {
        sensor = transform.Find("Sensor").GetComponent<SNSSector>();
        health = GetComponent<Health>();
        prevHealth = health.health;

        motor = GetComponent<BomberMotor>();

        state = STATE.Move;
        motor.setMoveSpeed(moveSpeed);
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
                handleMove();
                break;
            case STATE.Attack:
                handleAttack();
                break;
            case STATE.Hurt:
                break;
            case STATE.Dying:
                break;
            case STATE.Dead:
                break;
        }
    }

    /// <summary>
    /// Calls the motor to move the enemy, and if the sensor can see the target
    /// switches state to attack
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    void handleMove() {
        motor.MoveForward();
        if (sensor.CanSee(target))
            state = STATE.Attack;
    }

    /// <summary>
    /// Calls the motor to attack the target, and if the sensor can no longer
    /// see the target, switches state to move
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    void handleAttack() {
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
    }

}

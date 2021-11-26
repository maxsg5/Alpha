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
public class BomberController : MonoBehaviour
{
    public enum STATE {
        Move,
        Attack,
        Hurt,
        Dying,
        Dead
    };

    public _SNSSensor sensor;
    public Transform target;
    public float moveSpeed = 2.0f;

    private Enemy motor;
    private STATE state;
    private Rigidbody2D physics;
    private PathMove movement;
    private Weapon weapon;
    private Health health;
    private Animator animator;

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
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        motor = new BomberMotor(transform, physics, weapon, movement, health, animator);

        state = STATE.Move;
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

    void handleMove() {
        motor.MoveForward();
        if (sensor.CanSee(target))
            state = STATE.Attack;
    }

    void handleAttack() {
        motor.MoveForward();
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
    }
}

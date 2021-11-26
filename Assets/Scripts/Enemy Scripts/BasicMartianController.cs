using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SensorSystem;

/// <summary>
/// The human Basic Martian enemy class. Walks along the ground and shoots
/// at the player if it can see them
/// </summary>
/// 
/// Author: Josh Coss       (JC)
/// 
/// Variables
/// moveSpeed       Speed at which the enemy moves
public class BasicMartianController : MonoBehaviour
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
    public float moveSpeed = 6.0f;

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
    /// 2021-10-18  JC          Initial Testing
    /// 2021-10-25  JC          Added movement, weapon, and health, as well as
    ///                         implemented a motor script
    void Start()
    {
        movement = GetComponent<PathMove>();
        // This class uses a sector sensor
        sensor = transform.Find("Sensor").GetComponent<SNSSector>();
        physics = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        
        motor = new BasicMartianMotor(transform, physics, weapon, movement, health, animator);

        state = STATE.Move;
        movement.setMoveSpeed(moveSpeed);
    }

    /// <summary>
    /// Prints a test message to the console if the sensor can see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-18  JC          Initial Testing
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

    void handleAttack(){
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
    }
}

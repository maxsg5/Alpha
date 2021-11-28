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
[RequireComponent(typeof(BasicMartianMotor))]
public class BasicMartianController : MonoBehaviour
{
    public enum STATE {
        Move,
        Run,
        Attack,
        Hurt,
        Dying,
        Dead
    };

    private _SNSSensor sensor;
    public Transform target;
    public float moveSpeed = 3.0f;

    private BasicMartianMotor motor;
    private STATE state;
    private Health health;

    private float prevHealth;


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
        sensor = transform.Find("Sensor").GetComponent<SNSSector>();
        health = GetComponent<Health>();
        prevHealth = health.health;

        motor = GetComponent<BasicMartianMotor>();

        state = STATE.Move;
        motor.setMoveSpeed(moveSpeed);
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
            case STATE.Run:
                handleRun();
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
            state = STATE.Run;
    }

    void handleRun() {
        motor.setMoveSpeed(moveSpeed * 1.5f);
        motor.RunForward();
        if (!sensor.CanSee(target))
            state = STATE.Move;
    }

    void handleAttack(){
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collided with " + other.name);
        if (other.gameObject.tag == "Player") {
            state = STATE.Attack;
        }
    }
}

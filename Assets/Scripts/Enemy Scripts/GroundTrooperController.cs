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
/// STATE           enum of enemy states
/// sensor          _SNSSensor attached to this object
/// target          The object that the enemy is currently targeting
/// moveSpeed       Speed at which the enemy moves
/// motor           GroundTrooperMotor attached to this object
/// state           The current state of the enemy
/// health          The health of the enemy
/// prevHealth      The previous health of the enemy before it was damaged
[RequireComponent(typeof(GroundTrooperMotor))]
public class GroundTrooperController : MonoBehaviour
{
    public enum STATE {
        Move,
        Attack,
        Hurt,
        Dead
    };

    private _SNSSensor sensor;
    public Transform target;
    public float moveSpeed = 4.0f;

    private GroundTrooperMotor motor;
    public STATE state;
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
    /// 2021-10-25  JC          Added movement, weapon, and health, as well as
    ///                         implemented a motor script
    /// 2021-12-08  JC          Refactored after motor script was reworked
    private void Start()
    {
        // This class uses a sector sensor
        sensor = GetComponentInChildren<SNSSector>();
        health = GetComponent<Health>();
        prevHealth = health.health;

        motor = GetComponent<GroundTrooperMotor>();

        state = STATE.Move;
        motor.setSpeed(moveSpeed);
    }

    /// <summary>
    /// Switches the state of the enemy based on the current state
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-12-08  JC          Refactored after states were implemented
    private void Update()
    {
        switch (state) {
            case STATE.Move:
                handleMove();
                break;
            case STATE.Attack:
                handleAttack();
                break;
            case STATE.Hurt:
                handleGetHurt();
                break;
            case STATE.Dead:
                handleDeath();
                break;
        }
    }

    /// <summary>
    /// Calls the motor to move the enemy, and changes the state to attack if
    /// the enemy can see the target, or to hurt if prevHealth does not equal
    /// current health
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleMove() {
        motor.MoveForward();
        if (sensor.CanSee(target)) 
            state = STATE.Attack;
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    /// <summary>
    /// Calls the motor to attack the enemy, and changes the state to move if 
    /// the enemy can no longer see the target, or to hurt if prevHealth does not 
    /// equal current health
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleAttack() {
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    /// <summary>
    /// Starts the hurt timeout coroutine, and changes state to attack if the sensor
    /// can still see the target, or to move if the sensor can no longer see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleGetHurt() {
        StartCoroutine(HurtTimeout());

        if (sensor.CanSee(target))
            state = STATE.Attack;
        else if (!sensor.CanSee(target))
            state = STATE.Move;
    }

    /// <summary>
    /// Calls the motor to die
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleDeath() {
        motor.Death();
    }

    /// <summary>
    /// Calls the motor to get hurt, waits for .2 seconds, and changes prevHealth
    /// to current health. If current health is less than or equal to zero, changes
    /// to the dead state.
    /// </summary>
    /// <returns></returns>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private IEnumerator HurtTimeout() {
        motor.GetHurt();
        yield return new WaitForSeconds(0.2f);
        prevHealth = health.health;

        if (health.health <= 0) {
            state = STATE.Dead;
        }
    }



}

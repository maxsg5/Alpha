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
    /// Gets the movement, sensor, and rigidbody components of the enemy, as
    /// well as sets the turn and move speed
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Changed Rigidbody to Rigidbody2D
    /// 2021-10-25  JC          Added movement, weapon, and health, as well as
    ///                         implemented a motor script
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
    /// Prints a test message to the console if the sensor can see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
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

    private void handleMove() {
        motor.MoveForward();
        if (sensor.CanSee(target)) 
            state = STATE.Attack;
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    private void handleAttack() {
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    private void handleGetHurt() {
        StartCoroutine(HurtTimeout());

        if (sensor.CanSee(target))
            state = STATE.Attack;
        else if (!sensor.CanSee(target))
            state = STATE.Move;
    }

    private void handleDeath() {
        motor.Death();
    }

    private IEnumerator HurtTimeout() {
        motor.GetHurt();
        yield return new WaitForSeconds(0.2f);
        prevHealth = health.health;

        if (health.health <= 0) {
            state = STATE.Dead;
        }
    }



}

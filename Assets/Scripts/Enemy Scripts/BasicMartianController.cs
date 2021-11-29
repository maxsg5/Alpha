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
        Cooldown,
        Hurt,
        Dying,
        Dead
    };

    private _SNSSensor sector_sensor;
    public Transform target;
    public float moveSpeed = 3.0f;
    public float timer = 84.0f / 60.0f;
    private bool attackMode;
    private bool cooling;
    private float intTimer;

    private BasicMartianMotor motor;
    public STATE state;
    private Health health;
    private AttackTrigger attack_trigger;

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
    private void Start()
    {
        sector_sensor = transform.Find("SectorSensor").GetComponent<SNSSector>();
        health = GetComponent<Health>();
        prevHealth = health.health;
        attack_trigger = GetComponentInChildren<AttackTrigger>();
        intTimer = timer;

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
    private void Update()
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
            case STATE.Cooldown:
                handleCooldown();
                break;
            case STATE.Hurt:
                handleGetHurt();
                break;
            case STATE.Dying:
                handleDying();
                break;
            case STATE.Dead:
                handleDeath();
                break;
        }
    }

    private void handleMove() {
        motor.MoveForward();
        if (sector_sensor.CanSee(target)) {
            state = STATE.Run;
        }
        if (prevHealth != health.health)
            state = STATE.Hurt;
        
    }

    private void handleRun() {
        motor.setMoveSpeed(moveSpeed * 1.5f);
        motor.RunForward();
        if (!sector_sensor.CanSee(target))
            state = STATE.Move;
        if (attack_trigger.isInRange()) {
            state = STATE.Attack;
        }

    }

    private void handleAttack(){
        motor.Attack(attack_trigger.getPlayerCollider());
        cooling = true;

        if (!attack_trigger.isInRange()) 
            state = STATE.Run;
        else if (cooling == true) 
            state = STATE.Cooldown;
        
        if (prevHealth != health.health) {
            state = STATE.Hurt;
        }
        
    }

    private void handleCooldown() {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling) {
            cooling = false;
            timer = intTimer;
            if (attack_trigger.isInRange()) {
                state = STATE.Attack;
            } else {
                state = STATE.Run;
            }
        }
    }

    private void handleGetHurt() {
        StartCoroutine(HurtTimeout());

        if (attack_trigger.isInRange()) {
            state = STATE.Attack;
        } else if (!sector_sensor.CanSee(target)) {
            state = STATE.Move;
        }
    }

    private void handleDying() {
        Physics2D.IgnoreLayerCollision(8, 11, true);
        state = STATE.Dead;
        motor.setDeathTime(Time.time);
        
    }

    private void handleDeath() {
        
        if (motor.DyingInProgress()) {
            motor.Dying();
        }
        else if (motor.Dead()) {
            motor.Death();
        }
    }

    private IEnumerator HurtTimeout() {
        motor.GetHurt();
        yield return new WaitForSeconds(0.2f);
        prevHealth = health.health;

        if (health.health == 0) {
            state = STATE.Dying;
        } 
    }

}

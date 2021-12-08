using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.SensorSystem;

/// <summary>
/// The human Basic Martian enemy class. Walks along the ground and attacks
/// the player if it can see them
/// </summary>
/// 
/// Author: Josh Coss       (JC)
/// 
/// Variables
/// STATE                enum of enemy states
/// sector_sensor        the sector sensor attached to the enemy
/// target               the target of the enemy
/// moveSpeed            the speed at which the enemy moves
/// timer                the timer for the enemy's attack cooldown
/// cooling              bool whether the enemy is cooling down
/// intTimer             the initial timer for the enemy's attack cooldown
/// motor                the motor attached to the enemy
/// state                the current state of the enemy
/// health               the health of the enemy
/// attack_trigger       the attack trigger attached to the enemy
/// prevHealth           the previous health of the enemy
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
    private float timer = 84.0f / 60.0f;
    private bool cooling;
    private float intTimer;

    private BasicMartianMotor motor;
    public STATE state;
    private Health health;
    private AttackTrigger attack_trigger;

    private float prevHealth;
    


    /// <summary>
    /// Gets the sensor, health, prevHealth, attack trigger, and motor of the enemy.
    /// Also sets timer, state, and sets movement speed of enemy
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-18  JC          Initial Testing
    /// 2021-10-25  JC          Added movement, weapon, and health, as well as
    ///                         implemented a motor script
    /// 2021-12-08  JC          Refactored after motor script was reworked, and
    ///                         melee cooldown was added
    private void Start()
    {
        sector_sensor = GetComponentInChildren<SNSSector>();
        health = GetComponent<Health>();
        prevHealth = health.health;
        attack_trigger = GetComponentInChildren<AttackTrigger>();
        intTimer = timer;

        motor = GetComponent<BasicMartianMotor>();

        state = STATE.Move;
        motor.setMoveSpeed(moveSpeed);
    }

    /// <summary>
    /// Switches the state of the enemy based on the current state
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-18  JC          Initial Testing
    /// 2021-12-08  JC          Refactored after states were added
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

    /// <summary>
    /// Calls the motor to set movement speed and move enemy forward. If sector_sensor can
    /// see target, switches state to run. If target is in range, switches state to attack.
    /// If health changes, switches state to hurt.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleMove() {
        motor.setMoveSpeed(moveSpeed);
        motor.MoveForward();
        if (sector_sensor.CanSee(target)) 
            state = STATE.Run;
        else if (attack_trigger.isInRange()) 
            state = STATE.Attack;      
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    /// <summary>
    /// Calls the motor to speed up and run forward. If sector sensor can no longer see target, 
    /// switches state to move. If the target is in range, switch the state to attack.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleRun() {
        motor.setMoveSpeed(moveSpeed * 1.5f);
        motor.RunForward();
        if (!sector_sensor.CanSee(target))
            state = STATE.Move;
        if (attack_trigger.isInRange()) {
            state = STATE.Attack;
        }
    }

    /// <summary>
    /// Calls the motor to stop moving and to attack the target. Sets cooling to true at
    /// the beginning of the attack. If the target is no longer in range, switches the state
    /// to run. If cooling is true, switches state to cooldown. If health changes, switches
    /// state to hurt.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleAttack(){
        motor.setMoveSpeed(0);
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

    /// <summary>
    /// Decreases the timer by deltaTime. If timer is less than 0 and cooling is true,
    /// switches cooling to false and resets intTimer. Then, if target is in attack 
    /// range, switches state to attack, otherwise switches state to run.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
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

    /// <summary>
    /// Starts the hurt timeout coroutine, and changes state to attack if the target is in range, 
    /// or to move if the sensor can no longer see the target
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleGetHurt() {
        StartCoroutine(HurtTimeout());

        if (attack_trigger.isInRange()) {
            state = STATE.Attack;
        } else if (!sector_sensor.CanSee(target)) {
            state = STATE.Move;
        }
    }

    /// <summary>
    /// Turns off collisions to bullets, switches state to dead, and calls the motor to set death
    /// time
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleDying() {
        Physics2D.IgnoreLayerCollision(8, 11, true);
        state = STATE.Dead;
        motor.setDeathTime(Time.time);
        
    }

    /// <summary>
    /// If dying is in progress, calls motor to run Dying. If dead, calls motor to run Dead.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void handleDeath() {
        
        if (motor.DyingInProgress()) {
            motor.Dying();
        }
        else if (motor.Dead()) {
            motor.Death();
        }
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

        if (health.health == 0) {
            state = STATE.Dying;
        } 
    }

}

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
public class GroundTrooperController : MonoBehaviour
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

    private Enemy motor;
    private STATE state;
    private Rigidbody2D physics;
    private PathMove movement;
    private Weapon weapon;
    private Health health;
    private Animator animator;

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
    void Start()
    {
        movement = GetComponent<PathMove>();
        // This class uses a sector sensor
        sensor = transform.Find("Sensor").GetComponent<SNSSector>();
        physics = GetComponent<Rigidbody2D>();
        weapon = transform.Find("Weapon").GetComponent<Weapon_Single_Shot>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        prevHealth = health.health;

        motor = new GroundTrooperMotor(transform, physics, weapon, movement, health, animator);

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
                handleGetHurt();
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
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    void handleAttack() {
        motor.Attack();
        if (!sensor.CanSee(target))
            state = STATE.Move;
        if (prevHealth != health.health)
            state = STATE.Hurt;
    }

    void handleGetHurt() {
        StartCoroutine(HurtTimeout());

        if (sensor.CanSee(target))
            state = STATE.Attack;
        else if (!sensor.CanSee(target))
            state = STATE.Move;
    }

    private IEnumerator HurtTimeout() {
        motor.GetHurt();
        yield return new WaitForSeconds(0.2f);
        prevHealth = health.health;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Vector2 force = collision.attachedRigidbody.velocity;
            motor.knockback(force);
        }
    }

}

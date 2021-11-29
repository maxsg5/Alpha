using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BasicMartianMotor : MonoBehaviour
{
    private float DEATH_ANIMATION_TIME = (85.0f / 60.0f);
    private float death_start_time;

    private Rigidbody2D rigidbody;
    private PathMove movement;
    private Damage melee;
    private Animator animator;
    private bool first_shot = true;
    public GameObject deathEffect;


    void Awake() {
        movement = GetComponent<PathMove>();
        rigidbody = GetComponent<Rigidbody2D>();
        melee = transform.Find("Melee").GetComponent<Damage>();
        animator = GetComponentInChildren<Animator>();

        animator.SetBool("dead", false);
    }

    public void MoveForward()
    {
        animator.SetBool("attack", false);
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }

    public void RunForward() {
        animator.SetBool("attack", false);
        animator.SetBool("hurt", false);
        animator.SetBool("run", true);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }
    
    public void Attack(Collider2D player_collider)
    {
        animator.SetBool("attack", true);
        animator.SetFloat("speed", 0.0f);
        melee.doDamage(player_collider);

    }


    public void GetHurt()
    {
        Debug.Log(animator.GetBool("dead"));
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("hurt", true);
    }

    public void knockback(Vector2 force) {
        rigidbody.AddForce(new Vector2(0.3f * force.x, 2.0f), ForceMode2D.Impulse);
    }

    public void Death()
    {
        Instantiate(deathEffect, transform.position + new Vector3(0.0f, -0.4f, 0.0f), transform.rotation);
        Destroy(this.gameObject);
    }

    public void Dying() {        
        if (this.DyingInProgress()) {
            return;
        } 
    }

    public void setDeathTime(float time) {
        animator.SetBool("dead", true);
        animator.SetFloat("speed", 0.0f);
        death_start_time = time;
    }

    public bool DyingInProgress() {
        return Time.time - death_start_time < DEATH_ANIMATION_TIME;

    }

    public bool Dead() {
        if (Time.time - death_start_time < DEATH_ANIMATION_TIME + 1.0f)
            return true;
        else
            return false;
    }

    public void setMoveSpeed(float speed) {
        movement.setMoveSpeed(speed);
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Vector2 force = collision.attachedRigidbody.velocity;
            knockback(force);
        }
    }

}

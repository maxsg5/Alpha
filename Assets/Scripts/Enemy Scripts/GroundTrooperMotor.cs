using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class GroundTrooperMotor : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PathMove movement;
    private Weapon weapon;
    private Animator animator;
    public GameObject deathEffect;

    void Awake() {
        movement = GetComponent<PathMove>();
        rigidbody = GetComponent<Rigidbody2D>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon_Single_Shot>();
        animator = GetComponent<Animator>();
        
    }

    public void MoveForward() {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
        
    }

    public void Attack()
    {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 0.0f);
        weapon.Fire();
    }

    public void Idle() {
        animator.SetFloat("speed", 0.0f);
    }

    public void GetHurt() {
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("hurt", true);
    }

    public void Death() {
        Instantiate(deathEffect, transform.position + new Vector3(0.0f, 0.25f, 0.0f), transform.rotation);
        Destroy(this.gameObject);
    }

    public void knockback(Vector2 force) {
        rigidbody.AddForce(new Vector2(0.3f * force.x, 2.0f), ForceMode2D.Impulse);
    }


    public void setSpeed(float speed) {
        movement.setMoveSpeed(speed);
    }
    
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Vector2 force = collision.attachedRigidbody.velocity;
            knockback(force);
        }
    }
}

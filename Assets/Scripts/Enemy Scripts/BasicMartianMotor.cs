using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BasicMartianMotor : MonoBehaviour
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
        animator = GetComponentInChildren<Animator>();
        
    }

    public void MoveForward()
    {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }

    public void RunForward() {
        animator.SetBool("hurt", false);
        animator.SetBool("run", true);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }
    
    public void Attack()
    {
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("run", false);
        animator.SetBool("attack", true);
    }

    public void Idle()
    {
        throw new System.NotImplementedException();
    }

    public void GetHurt()
    {
        throw new System.NotImplementedException();
    }

    public void knockback(Vector2 force) {
        var magnitude = 5000;
        rigidbody.AddForce(force * magnitude);
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void setMoveSpeed(float speed) {
        movement.setMoveSpeed(speed);
    }
}

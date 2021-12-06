using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMotor : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PathMove movement;
    private Weapon weapon;
    private Animator animator;
    private ParticleSystem deathParticles;

    void Awake() {
        movement = GetComponent<PathMove>();
        rigidbody = GetComponent<Rigidbody2D>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon_Single_Shot>();
        animator = GetComponent<Animator>();
        //deathParticles = GameObject.Find("DeathParticles").GetComponent<ParticleSystem>();
    }

    public void MoveForward()
    {
        movement.Move(rigidbody);
    }

    public void Attack()
    {
        weapon.Fire();
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
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void setMoveSpeed(float speed) {
        movement.setMoveSpeed(speed);
    }
}

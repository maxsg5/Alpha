using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrooperMotor : Enemy
{

    public GroundTrooperMotor(Transform transform, Rigidbody2D rigidbody, Weapon weapon, 
                    PathMove movement, Health health, Animator animator):base(transform, rigidbody, weapon, 
                    movement, health, animator) {
        
    }

    public override void MoveForward() {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }

    public override void Attack()
    {
        animator.SetBool("hurt", false);
        animator.SetFloat("speed", 0.0f);
        weapon.Fire();
    }

    public override void Idle() {
        animator.SetFloat("speed", 0.0f);
    }

    public override void GetHurt() {
        animator.SetFloat("speed", 0.0f);
        animator.SetBool("hurt", true);
        //rigidbody.AddForce(new Vector2(0.3f, 0.2f) * transform.localScale.x, ForceMode2D.Impulse);
    }

    public override void knockback(Vector2 force) {
        rigidbody.AddForce(new Vector2(0.3f * force.x, 2.0f), ForceMode2D.Impulse);
    }
}

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
        animator.SetFloat("speed", 3.0f);
        movement.Move(rigidbody);
    }

    public override void Attack()
    {
        animator.SetFloat("speed", 0.0f);
        weapon.Fire();
    }

    public override void Idle() {
        animator.SetFloat("speed", 0.0f);
    }
}

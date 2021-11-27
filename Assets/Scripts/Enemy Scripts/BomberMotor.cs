using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMotor : Enemy
{

    public BomberMotor(Transform transform, Rigidbody2D rigidbody, Weapon weapon, 
                PathMove movement, Health health, Animator animator):base(transform, rigidbody, weapon, 
                movement, health, animator) {
        
    }

    public override void MoveForward()
    {
        movement.Move(rigidbody);
    }

    public override void Attack()
    {
        weapon.Fire();
    }

    public override void Idle()
    {
        throw new System.NotImplementedException();
    }

    public override void GetHurt()
    {
        throw new System.NotImplementedException();
    }
    
    public override void knockback(Vector2 force) {
        rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMartianMotor : Enemy
{
    
    public BasicMartianMotor(Transform transform, Rigidbody2D rigidbody, Weapon weapon,
                    PathMove movement, Health health):base(transform, rigidbody, weapon,
                    movement, health) {

    }

    public override void MoveForward()
    {
        movement.Move(rigidbody);
    }
    
    public override void Attack()
    {
        weapon.Fire();
    }
}

using System.Collections;
using System.Collections.Generic;
using Motors;
using UnityEngine;

public class Motor_Boss : Motor
{
	private static readonly int animator_die = Animator.StringToHash("Die");
	private static readonly int animator_walking = Animator.StringToHash("Walking");
	private static readonly int animator_jumping = Animator.StringToHash("Jumping");
	private static readonly int animator_attacking = Animator.StringToHash("Attacking");
	private static readonly int animator_taking_damage = Animator.StringToHash("Take Damage");

	public Motor_Boss(Transform transform, Rigidbody2D rigidbody, Animator animator)
		: base(transform, rigidbody, animator) { }

	public void Start_Jump()
	{
		this.animator.SetBool(animator_jumping, true);
	}

	public void End_Jump()
	{
		this.animator.SetBool(animator_jumping, false);
	}

	public void Start_Attack()
	{
		this.animator.SetBool(animator_attacking, true);
	}
	
	public void End_Attack()
	{
		this.animator.SetBool(animator_attacking, false);
	}

	public void Start_Walk()
	{
		this.animator.SetBool(animator_walking, true);
	}
	
	public void End_Walk()
	{
		this.animator.SetBool(animator_walking, false);
	}

	public void Take_Damage()
	{
		this.animator.SetTrigger(animator_taking_damage);

		bool sprite_flipped_x = this.rigidbody.gameObject.GetComponent<SpriteRenderer>().flipX;
		Vector3 force = new Vector3(0, 2, 0);
		if (sprite_flipped_x) {
			force.x = 2;
		}
		else {
			force.x = -2;
		}
		
		this.rigidbody.AddForce(force);
	}

	public void Die()
	{
		this.animator.SetTrigger(animator_die);
	}
	
	public override void MoveForward(Vector3 target_pos, float speed)
	{
		Vector2 target = new Vector2(target_pos.x, target_pos.y);
		Vector2 velocity = new Vector2(target_pos.x >= this.transform.position.x ? speed : -speed, 0);
		this.rigidbody.velocity = velocity;
	}
}

// Author: Declan Simkins

using UnityEngine;

namespace Motors
{
	public class Motor_Boss : Motor
	{
		// Convert animator property string to hashes for faster and more reliable reference
		private static readonly int animator_die = Animator.StringToHash("Die");
		private static readonly int animator_walking = Animator.StringToHash("Walking");
		private static readonly int animator_jumping = Animator.StringToHash("Jumping");
		private static readonly int animator_attacking = Animator.StringToHash("Attacking");
		private static readonly int animator_taking_damage = Animator.StringToHash("Take Damage");

		public Motor_Boss(Transform transform, Rigidbody2D rigidbody, Animator animator)
			: base(transform, rigidbody, animator) { }

		/// <summary>
		/// Start jump animation
		/// </summary>
		public void Start_Jump()
		{
			this.animator.SetBool(animator_jumping, true);
		}

		/// <summary>
		/// End jump animation
		/// </summary>
		public void End_Jump()
		{
			this.animator.SetBool(animator_jumping, false);
		}

		/// <summary>
		/// Start attack animation
		/// </summary>
		public void Start_Attack()
		{
			this.animator.SetBool(animator_attacking, true);
		}
	
		/// <summary>
		/// End attack animation
		/// </summary>
		public void End_Attack()
		{
			this.animator.SetBool(animator_attacking, false);
		}

		/// <summary>
		/// Start walking animation
		/// </summary>
		public void Start_Walk()
		{
			this.animator.SetBool(animator_walking, true);
		}
	
		/// <summary>
		/// End walking animation
		/// </summary>
		public void End_Walk()
		{
			this.animator.SetBool(animator_walking, false);
		}

		/// <summary>
		/// Start take damage animation and apply knockback effect
		/// </summary>
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

		/// <summary>
		/// Play death animation and play death audio
		/// </summary>
		public void Die()
		{
			GameObject.Find("BossScream2").GetComponent<AudioSource>().Play(); //added by Max Schafer: 2021-12-06
			this.animator.SetTrigger(animator_die);
		}
		
		public override void Move(Vector3 target_pos, float speed)
		{
			bool player_to_the_right = target_pos.x >= this.transform.position.x;
			Vector2 velocity = new Vector2(player_to_the_right? speed : -speed, this.rigidbody.velocity.y);
			this.sprite_renderer.flipX = player_to_the_right;
			this.rigidbody.velocity = velocity;
		}
	}
}

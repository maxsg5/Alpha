// Author: Declan Simkins

using UnityEngine;

namespace Motors
{
	// Handles movement and animation of the spider queen
	public class Motor_SQ : Motor
	{
		// Cache animator properties for more efficient and reliable lookup
		private static readonly int Anim_Attacking = Animator.StringToHash("Attacking");
		private static readonly int Anim_Jumping = Animator.StringToHash("Jumping");
		private static readonly int Anim_Moving = Animator.StringToHash("Moving");
		private static readonly int Anim_Die = Animator.StringToHash("Die");
		private static readonly int Anim_Taking_Damage = Animator.StringToHash("Taking_Damage");

		private float anim_attack_changed = Time.time;

		public Motor_SQ(Transform transform, Rigidbody rb, Animator anim, Motor.Data data)
			: base(transform, rb, anim, data) { }
		
		public override void MoveForward(Vector3 target_pos, float speed)
		{
			this.Move(target_pos, speed);
			this.animator.SetBool(Anim_Moving, true);
		}

		public override void MoveBackward(Vector3 target_pos, float speed)
		{
			this.MoveForward(target_pos, -speed);
			this.animator.SetBool(Anim_Moving, true);
		}

		/// <summary>
		/// Stops walking animation.
		/// </summary>
		public void Stop_Moving()
		{
			this.animator.SetBool(Anim_Moving, false);
		}

		/// <summary>
		/// Starts attack animation.
		/// </summary>
		public void Start_Attack()
		{
			// Don't switch animator state more than once in the same frame
			if (Time.time == this.anim_attack_changed) {
				return;
			}
			
			this.animator.SetBool(Anim_Attacking, true);
			this.anim_attack_changed = Time.time;
		}
	
		/// <summary>
		/// Stops attack animation.
		/// </summary>
		public void End_Attack()
		{
			// Don't switch animator state more than once in the same frame
			if (Time.time == this.anim_attack_changed) {
				return;
			}
			
			this.animator.SetBool(Anim_Attacking, false);
			this.anim_attack_changed = Time.time;
		}

		/// <summary>
		/// Start jump animation.
		/// </summary>
		public void Start_Jump()
		{
			this.animator.SetBool(Anim_Jumping, true);
		}

		/// <summary>
		/// End jump animation.
		/// </summary>
		public void End_Jump()
		{
			this.animator.SetBool(Anim_Jumping, false);
		}

		/// <summary>
		/// Start take damage animation.
		/// </summary>
		public void Take_Damage()
		{
			this.animator.SetBool(Anim_Taking_Damage, true);
		}

		/// <summary>
		/// End take damage animation.
		/// </summary>
		public void Stop_Take_Damage()
		{
			
			this.animator.SetBool(Anim_Taking_Damage, false);
		}

		/// <summary>
		/// Start death animation.
		/// </summary>
		public void Die()
		{
			this.animator.SetTrigger(Anim_Die);
		}
	}
}

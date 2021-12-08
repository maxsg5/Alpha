// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	/// <summary>
	/// Behaviour state for when the boss takes damage; plays an animation,
	/// applies a knockback, and exits on a timer
	/// </summary>
	public class State_Boss_Taking_Damage : State_Boss
	{
		private float animation_duration = 0.5f;
		private float enter_time;

		public State_Boss_Taking_Damage(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }

		/// <summary>
		/// Switches states to jumping (as it will likely be in the air from
		/// the knockback) if the animation timer has expired
		/// </summary>
		public override void Tick()
		{
			if (Time.time >= this.enter_time + this.animation_duration) {
				this.controller.Switch_States(this.controller.State_Jumping);
			}
		}

		/// <summary>
		/// Records time at which the state was entered and begins
		/// knocback and take damage animations
		/// </summary>
		public override void Enter()
		{
			this.enter_time = Time.time;
			this.motor.Take_Damage();
		}

		public override void Exit()
		{
			return;
		}
	}
}
// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	public class State_SQ_Tracking : State_SQ
	{
		public State_SQ_Tracking(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor) { }

		public override void Enter()
		{
			return;
		}

		/// <summary>
		/// Jumps if in jump range, attacks if in attack range, and
		/// moves closer / further if it's too close / far for either.
		/// </summary>
		public override void Tick()
		{
			Vector3 to_target = this.controller.Target.position - this.controller.transform.position;
			bool can_jump = to_target.sqrMagnitude <= this.controller.Jump_Range2;
			bool in_attack_range = to_target.sqrMagnitude <= this.controller.Attack_Range_Max2;
			bool too_close = to_target.sqrMagnitude <= this.controller.Attack_Range_Min2;
			
			if (can_jump && !in_attack_range) {
				this.controller.Behaviour_State = this.controller.State_Jumping;
			}
			else if (!in_attack_range) {
				this.sq_motor.MoveForward(this.controller.Target.position, this.controller.Speed);
			}
			else if (too_close) {
				Debug.Log("Moving backward!");
				this.sq_motor.MoveBackward(this.controller.Target.position, this.controller.Speed);
			}
			else if (in_attack_range) {
				this.controller.Behaviour_State = this.controller.State_Attacking;
			}
		}

		/// <summary>
		/// Stops walking animation via the motor.
		/// </summary>
		public override void Exit()
		{
			this.sq_motor.Stop_Moving();
		}
	}
}

// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	public class State_SQ_Take_Damage : State_SQ
	{
		private const float anim_time = 0.375f;
		
		private float start_time;
		private float end_time;
		private State_SQ prev_prev_state;

		public State_SQ_Take_Damage(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor) { }

		/// <summary>
		/// Records start time of the take damage animation and starts
		/// the animation via the motor.
		/// </summary>
		public override void Enter()
		{
			this.start_time = Time.time;
			this.end_time = this.start_time + State_SQ_Take_Damage.anim_time;
			this.sq_motor.Take_Damage();
		}

		/// <summary>
		/// Switches states to the previous state when the end time
		/// has been reached. Does not update the next state's previous state
		/// to this state.
		/// </summary>
		public override void Tick()
		{
			if (!(Time.time >= this.end_time)) {
				return;
			}
			
			this.controller.Switch_States(this.prev_state, false);
		}

		/// <summary>
		/// Stops the take damage animation via the motor.
		/// </summary>
		public override void Exit()
		{
			this.sq_motor.Stop_Take_Damage();
		}
	}
}

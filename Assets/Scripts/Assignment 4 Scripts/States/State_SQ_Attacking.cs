// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	/// <summary>
	/// Controls the spider queen's behaviour while attacking.
	/// </summary>
	public class State_SQ_Attacking : State_SQ
	{
		private float start_time;
		private float end_time;

		public State_SQ_Attacking(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor) { }

		/// <summary>
		/// Records start time of the attack and start the attack via the
		/// spider queen's motor.
		/// </summary>
		public override void Enter()
		{
			this.start_time = Time.time;
			this.end_time = this.start_time + this.controller.Jump_Time;
			this.sq_motor.Start_Attack();
		}

		/// <summary>
		/// Checks if the end time of the animation has been reached yet.
		/// Transitions to the previous state if so.
		/// </summary>
		public override void Tick()
		{
			if (!(Time.time >= this.end_time)) {
				return;
			}

			this.controller.Behaviour_State = this.prev_state;
		}

		/// <summary>
		/// Ends the attack via the spider queen's motor.
		/// </summary>
		public override void Exit()
		{
			this.sq_motor.End_Attack();
		}
	}
}

// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	public class State_SQ_Jumping : State_SQ
	{
		private float start_time;
		private float end_time;
		
		public State_SQ_Jumping(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor) { }

		/// <summary>
		/// Records the start time of the jump and starts the jump via
		/// the motor.
		/// </summary>
		public override void Enter()
		{
			this.start_time = Time.time;
			this.end_time = this.start_time + this.controller.Jump_Time;
			this.sq_motor.Start_Jump();
		}

		/// <summary>
		/// Moves the spider queen towards the controller's target at the
		/// controller's jump speed and checks to see if the end time of the
		/// jump has been reached yet, transitioning to the previous state if
		/// it has been reached.
		/// </summary>
		public override void Tick()
		{
			if (!(Time.time >= this.end_time)) {
				this.sq_motor.MoveForward(this.controller.Target.position, this.controller.Jump_Speed);
				return;
			}
			
			this.controller.Behaviour_State = this.prev_state;
		}

		/// <summary>
		/// End the jump via the motor.
		/// </summary>
		public override void Exit()
		{
			this.sq_motor.End_Jump();
		}
	}
}

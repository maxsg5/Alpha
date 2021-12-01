// Author: Declan Simkins

using Controllers;
using Motors;

namespace States
{
	public class State_SQ_Die : State_SQ
	{
		public State_SQ_Die(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor) { }
	
		/// <summary>
		/// Triggers the spider queen's death animation via the motor.
		/// </summary>
		public override void Enter()
		{
			this.sq_motor.Die();
		}
	
		public override void Tick()
		{
			return;
		}


		public override void Exit()
		{
			return;
		}
	}
}

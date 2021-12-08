// Author: Declan Simkins

using Controllers;
using Motors;

namespace States
{
	/// <summary>
	/// Behaviour state for when the boss is jumping
	/// </summary>
	public class State_Boss_Jumping : State_Boss
	{
		public State_Boss_Jumping(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }

		/// <summary>
		/// Switch to following state if the boss is touching the  ground
		/// </summary>
		public override void Tick()
		{
			if (this.controller.Grounded) {
				this.controller.Switch_States(this.controller.State_Following);
			}
		}

		/// <summary>
		/// Start jump animation 
		/// </summary>
		public override void Enter()
		{
			this.motor.Start_Jump();
		}

		/// <summary>
		/// End jump animation
		/// </summary>
		public override void Exit()
		{
			this.motor.End_Jump();
		}
	}
}
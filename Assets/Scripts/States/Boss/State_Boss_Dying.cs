// Author: Declan Simkins

using Controllers;
using Motors;

namespace States
{
	/// <summary>
	/// Behaviour state for when the boss is dying
	/// </summary>
	public class State_Boss_Dying : State_Boss
	{
		public State_Boss_Dying(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }

		public override void Tick()
		{
			return;
		}

		/// <summary>
		/// Play death animations.
		/// </summary>
		public override void Enter()
		{
			this.motor.Die();
		}

		public override void Exit()
		{
			return;
		}
	}
}
// Author: Declan Simkins

using Controllers;
using Motors;

namespace States
{
	/// <summary>
	/// Represents a state within the game's boss's state machine
	/// </summary>
	public abstract class State_Boss : State
	{
		protected Controller_Boss controller;
		protected Motor_Boss motor;

		public State_Boss(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state)
		{
			this.controller = controller;
			this.motor = motor;
		}
	}
}
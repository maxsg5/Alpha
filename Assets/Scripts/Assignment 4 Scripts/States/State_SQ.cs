// Author: Declan Simkins

using Controllers;
using Motors;

namespace States
{
	/// <summary>
	/// State with additional fields required for
	/// controlling the spider queen.
	/// </summary>
	public abstract class State_SQ : State
	{
		protected Controller_SQ controller;
		protected Motor_SQ sq_motor;
		
		protected State_SQ(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state)
		{
			this.controller = controller;
			this.sq_motor = sq_motor;
		}
	}
}


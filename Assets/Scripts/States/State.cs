// Author: Declan Simkins

namespace States
{
	/// <summary>
	/// Represents a state within a state machine. Defines behaviour within
	/// that state.
	/// </summary>
	public abstract class State
	{
		public State prev_state;
	
		public State() { }
	
		public State(State prev_state)
		{
			this.prev_state = prev_state;
		}

		/// <summary>
		/// Performs the state's "per frame" actions though it
		/// does not necessarily have to be called each frame.
		/// </summary>
		public abstract void Tick();
		
		/// <summary>
		/// Called when the state is transitioned to.
		/// </summary>
		public abstract void Enter();
		
		/// <summary>
		/// Called when the state is transitioned out of.
		/// </summary>
		public abstract void Exit();
	}
}

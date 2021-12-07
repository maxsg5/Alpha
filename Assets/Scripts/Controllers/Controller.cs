// Author: Declan Simkins

using System.Collections.Generic;
using Motors;
using UnityEngine;

using States;

namespace Controllers
{
	public abstract class Controller : MonoBehaviour
	{
		protected readonly static State_Null null_state = new State_Null();
		protected State behaviour_state;

		protected Motor motor;
		protected Rigidbody2D rb;
		protected Animator animator;

		/// <summary>
		/// Provides public access to the controller's state but enforces
		/// setting to be done in such a way that the states are entered
		/// and exited properly.
		/// </summary>
		public State Behaviour_State
		{
			get => this.behaviour_state;
			set
			{
				this.behaviour_state.Exit();
				value.Enter();
				value.prev_state = this.behaviour_state;
				this.behaviour_state = value;
			}
		}

		public Rigidbody2D RB => this.rb;

		/// <summary>
		/// Defaults the controller's state to a null state
		/// Virtual so it can be overriden in child classes
		/// </summary>
		protected virtual void Awake()
		{
			this.behaviour_state = Controller.null_state;
			this.rb = this.GetComponent<Rigidbody2D>();
			this.animator = this.GetComponent<Animator>();
			
			this.Initialise_Motor();
			this.Initialise_States();
		}

		/// <summary>
		/// Ticks the controller's state
		/// Virtual so it can be overriden in child classes
		/// </summary>
		protected virtual void Update()
		{
			this.behaviour_state.Tick();
		}

		protected abstract void Initialise_Motor();
		protected abstract void Initialise_States();

		/// <summary>
		/// A dedicated method for switching states rather than using
		/// the Behaviour_State property; allows further control over state
		/// transitions by giving the option to not update the new state's
		/// previous state. Useful for things like taking damage where you
		/// don't want the state that transitions to it to remember that
		/// it's previous state was "take damage". 
		/// </summary>
		/// <param name="new_state">State to be transitioned to.</param>
		/// <param name="update_prev_state">
		/// Whether or not to set the new state's previous state to the
		/// controller's current state.
		/// </param>
		public void Switch_States(State new_state, bool update_prev_state = true)
		{
			this.behaviour_state.Exit();
			
			// check if the state is going to be dying. If so, remove the collider so we can't trigger hit anymore. Added by Max Schafer : 2021-12-07
			if (new_state.GetType().Name =="State_Boss_Dying")
			{
				this.GetComponent<Collider2D>().enabled = false;
			}

			new_state.Enter();
			
			if (update_prev_state) {
				new_state.prev_state = this.behaviour_state;
			}
			this.behaviour_state = new_state;
		}
	}
}

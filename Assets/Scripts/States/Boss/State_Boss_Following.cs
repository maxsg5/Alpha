// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	/// <summary>
	/// Behaviour state for when the boss is following the player
	/// </summary>
	public class State_Boss_Following : State_Boss
	{
		private GameObject player;

		public State_Boss_Following(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }

		/// <summary>
		/// Switch to attacking state if close enough, otherwise move closer to player
		/// </summary>
		public override void Tick()
		{
			float distance_to_player = Vector3.Distance(
				this.player.transform.position
				, this.controller.transform.position
			);
			
			if (distance_to_player <= this.controller.Attack_Range) {
				this.controller.Switch_States(this.controller.State_Attacking);
			}
			else {
				this.motor.Move(this.player.transform.position, this.controller.Speed);
			}
		}
		
		/// <summary>
		/// Start walking animation and find player 
		/// </summary>
		public override void Enter()
		{
			this.motor.Start_Walk();
			this.player = GameObject.FindWithTag("Player");
		}

		/// <summary>
		/// Stop walking animation
		/// </summary>
		public override void Exit()
		{
			this.motor.End_Walk();
		}
	}
}
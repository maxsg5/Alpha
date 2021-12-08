// Author: Declan Simkins

using System.Collections;
using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	/// <summary>
	/// Behaviour state to play the boss's intro sequence
	/// </summary>
	public class State_Boss_Intro : State_Boss
	{
		public State_Boss_Intro(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }
		
		public override void Tick()
		{
			return;
		}

		/// <summary>
		/// Play boss's intro sequence and then switch to jumping / falling state
		/// </summary>
		public override void Enter()
		{
			this.controller.StartCoroutine(this.Delayed_Intro());
		}

		public override void Exit()
		{
			return;
		}

		private IEnumerator Delayed_Intro()
		{
			yield return new WaitForSeconds(1.5f);
			this.controller.RB.gravityScale = 1;
			this.controller.gameObject.GetComponent<Health>().enabled = true;
			this.controller.Switch_States(this.controller.State_Jumping);
		}
	}
}
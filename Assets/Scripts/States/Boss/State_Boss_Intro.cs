using System.Collections;
using System.Collections.Generic;
using Controllers;
using Motors;
using States;
using UnityEngine;

namespace States
{
	public class State_Boss_Intro : State_Boss
	{
		public State_Boss_Intro(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor)
		{ }
		
		public override void Tick()
		{
			return;
		}

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
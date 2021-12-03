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
			throw new System.NotImplementedException();
		}

		public override void Enter()
		{
			this.controller.RB.gravityScale = Physics.gravity.y;
			this.controller.gameObject.GetComponent<Health>().enabled = true;
			this.controller.Switch_States(this.controller.State_Jumping);
		}

		public override void Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
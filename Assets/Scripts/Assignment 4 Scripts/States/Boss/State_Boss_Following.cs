using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class State_Boss_Following : State_Boss
{
	public State_Boss_Following(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor) { }
	
	public override void Tick()
	{
		// move toward player
		// if in attack range
		//   this.controller.Switch_States(this.controller.State_Attacking);
		return;
	}

	public override void Enter()
	{
		this.motor.Start_Walk();
	}

	public override void Exit()
	{
		this.motor.End_Walk();
	}
}

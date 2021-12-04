using System.Collections;
using System.Collections.Generic;
using Controllers;
using Motors;
using States;
using UnityEngine;

public class State_Boss_Jumping : State_Boss
{
	public State_Boss_Jumping(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor) { }
	
	public override void Tick()
	{
		// if is grounded
		this.controller.Switch_States(this.controller.State_Following);
	}

	public override void Enter()
	{
		this.motor.Start_Jump();
	}

	public override void Exit()
	{
		this.motor.End_Jump();
	}
}

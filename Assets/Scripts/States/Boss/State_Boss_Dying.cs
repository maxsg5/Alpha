using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class State_Boss_Dying : State_Boss
{
	public State_Boss_Dying(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor) { }
	
	public override void Tick()
	{
		return;
	}

	public override void Enter()
	{
		this.motor.Die();
	}

	public override void Exit()
	{
		return;
	}
}

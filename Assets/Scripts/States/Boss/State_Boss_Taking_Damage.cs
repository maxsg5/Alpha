using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class State_Boss_Taking_Damage : State_Boss
{
	private float animation_duration = 0.5f;
	private float enter_time;

	public State_Boss_Taking_Damage(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor) { }
	
	public override void Tick()
	{
		if (Time.time >= this.enter_time + this.animation_duration) {
			this.controller.Switch_States(this.controller.State_Jumping);
		}
	}

	public override void Enter()
	{
		this.enter_time = Time.time;
		this.motor.Take_Damage();
	}

	public override void Exit()
	{
		return;
	}
}

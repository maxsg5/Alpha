using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;

public class State_Boss_Following : State_Boss
{
	private GameObject player;
	
	public State_Boss_Following(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state, controller, motor) { }
	
	public override void Tick()
	{
		float distance_to_player = Vector3.Distance(this.player.transform.position, this.controller.transform.position);
		if (distance_to_player <= this.controller.Attack_Range) {
			this.controller.Switch_States(this.controller.State_Attacking);
		}
		else {
			this.motor.Move(this.player.transform.position, this.controller.Speed);
		}
	}

	public override void Enter()
	{
		this.motor.Start_Walk();
		this.player = GameObject.FindWithTag("Player");
	}

	public override void Exit()
	{
		this.motor.End_Walk();
	}
}

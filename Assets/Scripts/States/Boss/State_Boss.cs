using System.Collections;
using System.Collections.Generic;
using Controllers;
using Motors;
using States;
using UnityEngine;

public abstract class State_Boss : State
{
	protected Controller_Boss controller;
	protected Motor_Boss motor;

	public State_Boss(State prev_state, Controller_Boss controller, Motor_Boss motor) : base(prev_state)
	{
		this.controller = controller;
		this.motor = motor;
	}
}

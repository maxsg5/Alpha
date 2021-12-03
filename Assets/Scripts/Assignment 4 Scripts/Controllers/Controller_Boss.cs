using System.Collections;
using System.Collections.Generic;
using Controllers;
using States;
using UnityEngine;

public class Controller_Boss : Controllers.Controller
{
	[SerializeField] private Tank tank;
	[SerializeField] private float attack_range;

	private Motor_Boss motor_boss;
	private State state_intro;
	private State state_jumping;
	private State state_following;
	private State state_attacking;
	private State state_taking_damage;

	public State State_Intro => this.state_intro;
	public State State_Jumping => this.state_jumping;
	public State State_Following => this.state_following;
	public State State_Attacking => this.state_attacking;

	public float Attack_Range => this.attack_range;

	protected override void Awake()
	{
		base.Awake();
		this.motor_boss = this.motor as Motor_Boss;
		this.tank.Sequence_Done += On_Tank_Sequence_Done;
	}

	protected override void Initialise_States()
	{
		this.state_intro = new State_Boss_Intro(
			Controller.null_state,
			this,
			this.motor_boss
		);

		this.state_jumping = new State_Boss_Jumping(
			Controller.null_state,
			this,
			this.motor_boss
		);
		
		this.state_following = new State_Boss_Following(
			Controller.null_state,
			this,
			this.motor_boss
		);
		
		this.state_attacking = new State_Boss_Attacking(
			Controller.null_state,
			this,
			this.motor_boss
		);
		
		this.state_taking_damage = new State_Boss_Taking_Damage(
			Controller.null_state,
			this,
			this.motor_boss
		);
	}

	private void On_Tank_Sequence_Done()
	{
		this.Switch_States(this.state_intro);
	}
}

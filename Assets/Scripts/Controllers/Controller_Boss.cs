using System.Collections;
using System.Collections.Generic;
using Controllers;
using States;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Controller_Boss : Controllers.Controller
{
	[SerializeField] private Tank tank;
	[SerializeField] private float attack_range;
	[SerializeField] private float speed;

	private Motor_Boss motor_boss;
	private Health health;
	
	private State state_intro;
	private State state_jumping;
	private State state_following;
	private State state_attacking;
	private State state_taking_damage;
	private State state_dying;

	public State State_Jumping => this.state_jumping;
	public State State_Following => this.state_following;
	public State State_Attacking => this.state_attacking;


	public float Attack_Range => this.attack_range;
	public float Speed => this.speed;

	protected override void Awake()
	{
		base.Awake();
		this.motor_boss = this.motor as Motor_Boss;
		this.tank.Sequence_Done += this.On_Tank_Sequence_Done;
		this.health = this.GetComponent<Health>();
		this.health.Health_Changed += this.On_Health_Changed;
	}

	protected override void Initialise_States()
	{
		this.state_intro = new State_Boss_Intro(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);

		this.state_jumping = new State_Boss_Jumping(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);
		
		this.state_following = new State_Boss_Following(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);
		
		this.state_attacking = new State_Boss_Attacking(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);
		
		this.state_taking_damage = new State_Boss_Taking_Damage(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);
		
		this.state_dying = new State_Boss_Dying(
			Controller.null_state,
			this,
			this.motor as Motor_Boss
		);
	}

	private void On_Tank_Sequence_Done()
	{
		this.Switch_States(this.state_intro);
	}

	private void On_Health_Changed(float new_health)
	{
		if (new_health <= 0) {
			this.Switch_States(this.state_dying);
		}
		else {
			this.Switch_States(this.state_taking_damage, false);
		}
	}
}

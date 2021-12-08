// Author: Declan Simkins

using Motors;
using States;
using UnityEngine;

namespace Controllers
{
	/// <summary>
	/// Controller for the game's boss
	/// </summary>
	[RequireComponent(typeof(Health))]
	public class Controller_Boss : Controllers.Controller
	{
		[SerializeField] private Tank tank;
		[SerializeField] private float attack_range;
		[SerializeField] private float speed;

		private Motor_Boss motor_boss;
		private Health health;
		private bool grounded;
	
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
		public bool Grounded => this.grounded;
		
		protected override void Awake()
		{
			base.Awake();
			
			this.motor_boss = this.motor as Motor_Boss;
			this.health = this.GetComponent<Health>();
			this.health.Health_Changed += this.On_Health_Changed;
			this.tank.Sequence_Done += this.On_Tank_Sequence_Done;
		}

		protected override void Initialise_Motor()
		{
			this.motor = new Motor_Boss(this.transform, this.rb, this.animator);
			this.motor_boss = this.motor as Motor_Boss;
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
		
			this.state_dying = new State_Boss_Dying(
				Controller.null_state,
				this,
				this.motor_boss
			);
		}

		/// <summary>
		/// Event handler; switches into the boss's intro state
		/// </summary>
		private void On_Tank_Sequence_Done()
		{
			this.Switch_States(this.state_intro);
		}

		/// <summary>
		/// Event handler; switches to taking damage state or dying state if
		/// no health remains on the boss
		/// </summary>
		/// <param name="new_health">New current health of the health component</param>
		private void On_Health_Changed(float new_health)
		{
			if (new_health <= 0) {
				this.Switch_States(this.state_dying);
			}
			else {
				this.Switch_States(this.state_taking_damage, false);
			}
		}
		
		/// <summary>
		/// Records that the boss is now on the ground
		/// </summary>
		/// <param name="other">Other object involved in the collision</param>
		public void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
				this.grounded = true;
			}
		}

		/// <summary>
		/// Records that the boss is not on the ground
		/// </summary>
		/// <param name="other">Other object involved in the collision</param>
		public void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.layer ==  LayerMask.NameToLayer("Ground")) {
				this.grounded = false;
			}
		}
	}
}

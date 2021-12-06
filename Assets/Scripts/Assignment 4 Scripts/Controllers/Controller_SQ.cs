// Author: Declan Simkins

using UnityEngine;

using Motors;
using States;

// Handles behavioural aspects of Spider Queen
namespace Controllers
{
	/// <summary>
	/// Controls the Spider Queen's behaviour using a state machine
	/// Uses a motor to control the Spider Queen's movement and animations
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody))]
	public class Controller_SQ : Controller
	{
		[SerializeField] private float attack_range_min;
		[SerializeField] private float attack_range_max;
		[SerializeField] private float jump_range;
		[SerializeField] private float jump_time;
		private float jump_speed;
		private float ATTACK_RANGE_MIN2;
		private float ATTACK_RANGE_MAX2;
		private float JUMP_RANGE2;

		private State_SQ state_patrolling;
		private State_SQ state_tracking;
		private State_SQ state_attacking;
		private State_SQ state_jumping;
		private State_SQ state_taking_damage;
		private State_SQ state_dying;
		
		public State_SQ State_Patrolling => this.state_patrolling;
		public State_SQ State_Tracking => this.state_tracking;
		public State_SQ State_Attacking => this.state_attacking;
		public State_SQ State_Jumping => this.state_jumping;

		[SerializeField] private float speed;
		[SerializeField] private float patrol_point_range;
		[SerializeField] private Transform[] control_points;
		[SerializeField] private SectorSensor spider_eye;
		[SerializeField] private Transform target;
		
		private Motor_SQ motor;
		private Rigidbody rb;
		private Animator animator;

		public float Speed => this.speed;
		public float Patrol_Point_Range => this.patrol_point_range;
		public SectorSensor Spider_Eye => this.spider_eye;
		public Transform[] Control_Points => this.control_points;
		public Transform Target => this.target;

		/// <summary>
		/// Allows the attack range min to be changed at runtime while ensuring
		/// that the squared value will remain consistent.
		/// </summary>
		public float Attack_Range_Min
		{
			get => this.attack_range_min;
			set
			{
				this.attack_range_min = value;
				this.ATTACK_RANGE_MIN2 = value * value;
			}
		}
		public float Attack_Range_Max2 => this.ATTACK_RANGE_MAX2;

		/// <summary>
		/// Allows the attack range max to be changed at runtime while ensuring
		/// that the squared value will remain consistent.
		/// </summary>
		public float Attack_Range_Max
		{
			get => this.attack_range_max;
			set
			{
				this.attack_range_max = value;
				this.ATTACK_RANGE_MAX2 = value * value;
			}
		}
		public float Attack_Range_Min2 => this.ATTACK_RANGE_MIN2;

		/// <summary>
		/// Allows the jump range to be changed at runtime while ensuring
		/// that the squared value will remain consistent.
		/// </summary>
		public float Jump_Range
		{
			get => this.jump_range;
			set
			{
				this.jump_range = value;
				this.JUMP_RANGE2 = value * value;
			}
		}
		public float Jump_Range2 => this.JUMP_RANGE2;
		
		public float Jump_Time => this.jump_time;
		public float Jump_Speed => this.jump_speed;

		/// <summary>
		/// Initializes values and aligns fields and properties
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			this.Attack_Range_Max = this.attack_range_max;
			this.Attack_Range_Min = this.attack_range_min;
			this.Jump_Range = this.jump_range;
			this.jump_speed = this.jump_range / (this.jump_time * 1.5f);

			this.rb = this.GetComponent<Rigidbody>();
			this.animator = this.GetComponent<Animator>();
			
			Motor.Data motor_data = new Motor.Data()
			{
				maxForward = 4f,
				maxBackward = 2f,
				maxTurn = 90f,
				maxFwdAcceleration = 30.0f,
				maxRevAcceleration = 15.0f,
				maxDeceleration = 30.0f,
				ATTACK_ANIMATION = 0.792f,
				ATTACK_CYCLE = 1.792f
			};
			this.motor = new Motor_SQ(this.transform, this.rb, this.animator, motor_data);
			
			this.Initialise_States();
			this.Behaviour_State = this.state_patrolling;
		}

		/// <summary>
		/// Preloads all of the spider queen's states
		/// </summary>
		private void Initialise_States()
		{
			this.state_patrolling = new State_SQ_Patrolling(
				Controller.null_state
				, this
				, this.motor
			);

			this.state_tracking = new State_SQ_Tracking(
				Controller.null_state
				, this
				, this.motor
			);

			this.state_jumping = new State_SQ_Jumping(
				Controller.null_state
				, this
				, this.motor
			);

			this.state_attacking = new State_SQ_Attacking(
				Controller.null_state
				, this
				, this.motor
			);

			this.state_taking_damage = new State_SQ_Take_Damage(
				Controller.null_state
				, this
				, this.motor
			);

			this.state_dying = new State_SQ_Die(
				Controller.null_state
				, this
				, this.motor
			);
		}

		/// <summary>
		/// Enters the take damage state, playing the take damage animation
		/// </summary>
		public void Take_Damage()
		{
			if (this.Behaviour_State == this.state_taking_damage) {
				return;
			}
			this.Behaviour_State = this.state_taking_damage;
		}

		/// <summary>
		/// Enters the dying state and plays the dying animation
		/// </summary>
		public void Die()
		{
			this.Behaviour_State = this.state_dying;
		}
	}
}

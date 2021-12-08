// Author: Declan Simkins

using Controllers;
using Motors;
using UnityEngine;

namespace States
{
	/// <summary>
	/// Behaviour state for when the boss is attacking.
	/// </summary>
	public class State_Boss_Attacking : State_Boss
	{
		public float attackRate = 1.0f; // The time between attacks
		private float lastAttack = 0f; // The time of the last attack

		public AudioSource attackSound; // The sound played when the boss attacks //added by Max Schafer: 2021-12-06
		private GameObject player;

		public State_Boss_Attacking(State prev_state, Controller_Boss controller, Motor_Boss motor)
			: base(prev_state, controller, motor) { }

		
		/// <summary>
		/// Switches state to following if the player is too far away to attack.
		/// </summary>
		public override void Tick()
		{
			float distance_to_player = Vector3.Distance(
				this.controller.transform.position
				, this.player.transform.position
			);
			
			if (distance_to_player > this.controller.Attack_Range) {
				this.controller.Switch_States(this.controller.State_Following);
			}
		}

		/// <summary>
		/// Finds the audio source to be played upon attacking and the player 
		/// </summary>
		public override void Enter()
		{
			//added by Max Schafer: 2021-12-06
			//find the gameobject with the name "BossScream1Looped"
			this.attackSound = GameObject.Find("BossScream1Looped").GetComponent<AudioSource>();

			//added by Max Schafer: 2021-12-06
			//only attack if the fireRate has passed CURRENTLY NOT WORKING
			if (Time.time > attackRate + lastAttack) {
				this.motor.Start_Attack();
				this.attackSound.Play();
				lastAttack = Time.time;
			}

			this.player = GameObject.FindWithTag("Player");
		}

		/// <summary>
		/// Stop attack audio and animations
		/// </summary>
		public override void Exit()
		{
			this.attackSound.Stop();
			this.motor.End_Attack();
		}
	}
}
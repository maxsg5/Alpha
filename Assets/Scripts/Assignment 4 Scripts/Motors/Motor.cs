// Author: Brian Brookwell, with modifications by Declan Simkins

using UnityEngine;

namespace Motors
{
	public abstract class Motor
	{
		/// <summary>
		/// Encapsulates data for the motor to reduce number of parameters
		/// in the constructor.
		/// </summary>
		public struct Data
		{
			public float maxForward, maxBackward, maxTurn;
			public float maxFwdAcceleration, maxRevAcceleration, maxDeceleration;
			public float ATTACK_ANIMATION, ATTACK_CYCLE;
		}

		protected readonly Transform transform;
		protected readonly Rigidbody rigidbody;
		protected readonly Animator animator;
		protected float maxForward, maxBackward, maxTurn;
		protected float maxFwdAcceleration, maxRevAcceleration, maxDeceleration;
		protected readonly float ATTACK_ANIMATION, ATTACK_CYCLE;
		protected float TURN_SPEED_RADIANS;

		/// <summary>
		/// Grabs data from the Data struct and initializes values.
		/// </summary>
		/// <param name="transform">Transform of the game object.</param>
		/// <param name="rigidbody">Rigidbody of the game object.</param>
		/// <param name="animator">Animator of the game object.</param>
		/// <param name="motorData">Data struct containing necessary data for the motor.</param>
		public Motor(Transform transform
			, Rigidbody rigidbody
			, Animator animator
			, Motor.Data motorData)
		{
			this.transform = transform;
			this.rigidbody = rigidbody;
			this.animator = animator;
			this.maxForward = motorData.maxForward;
			this.maxBackward = motorData.maxBackward;
			this.maxTurn = motorData.maxTurn;
			this.maxFwdAcceleration = motorData.maxFwdAcceleration;
			this.maxRevAcceleration = motorData.maxRevAcceleration;
			this.maxDeceleration = motorData.maxDeceleration;
			this.ATTACK_ANIMATION = motorData.ATTACK_ANIMATION;
			this.ATTACK_CYCLE = motorData.ATTACK_CYCLE;

			this.TURN_SPEED_RADIANS = this.maxTurn * Mathf.Deg2Rad;
		}

		/// <summary>
		/// Moves the transform towards `target_pos` at a specified speed
		/// while adhering to turn speed limits.
		/// </summary>
		/// <param name="target_pos">Position to move transform towards.</param>
		/// <param name="speed">Speed at which to move the transform.</param>
		public abstract void MoveForward(Vector3 target_pos, float speed);
	
		/// <summary>
		///	Moves the transform away from `target_pos` at a specified speed
		/// while adhering to turn speed limits. 
		/// </summary>
		/// <param name="targetPos">Position to move transform away from.</param>
		/// <param name="speed">Speed at which to move the transform.</param>
		public abstract void MoveBackward(Vector3 targetPos, float speed);

		/// <summary>
		/// Moves and rotates the transform towards the target position at a
		/// specified speed.
		/// </summary>
		/// <param name="targetPos">Position to move transform towards.</param>
		/// <param name="speed">Speed at which to move the transform.</param>
		protected void Move(Vector3 targetPos, float speed)
		{
			Vector3 targetDirection = targetPos - this.transform.position;
			targetDirection.y = 0.0f;
		
			Vector3 newDirection = Vector3.RotateTowards(
				this.transform.forward
				, targetDirection
				, this.TURN_SPEED_RADIANS * Time.deltaTime
				, 0.0f
			);
			this.transform.rotation = Quaternion.LookRotation(newDirection);

			// Rigidbody velocity not working, using translate instead
			Vector3 velocity = Vector3.forward * (speed * Time.deltaTime);
			this.transform.Translate(velocity);
		}
	}
}

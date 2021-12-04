// Author: Brian Brookwell, with modifications by Declan Simkins

using UnityEngine;

namespace Motors
{
	public abstract class Motor
	{
		protected readonly Transform transform;
		protected readonly Rigidbody2D rigidbody;
		protected readonly Animator animator;

		/// <summary>
		/// Grabs data from the Data struct and initializes values.
		/// </summary>
		/// <param name="transform">Transform of the game object.</param>
		/// <param name="rigidbody">Rigidbody of the game object.</param>
		/// <param name="animator">Animator of the game object.</param>
		/// <param name="motorData">Data struct containing necessary data for the motor.</param>
		public Motor(Transform transform
			, Rigidbody2D rigidbody
			, Animator animator)
		{
			this.transform = transform;
			this.rigidbody = rigidbody;
			this.animator = animator;
		}

		/// <summary>
		/// Moves the transform towards `target_pos` at a specified speed
		/// </summary>
		/// <param name="target_pos">Position to move transform towards.</param>
		/// <param name="speed">Speed at which to move the transform.</param>
		public abstract void MoveForward(Vector3 target_pos, float speed);
	}
}

// Author: Declan Simkins

using UnityEngine;

namespace Motors
{
	/// <summary>
	/// Base class for a motor which is responsible for animation and movement
	/// of a given transform
	/// </summary>
	public abstract class Motor
	{
		protected readonly Transform transform;
		protected readonly Rigidbody2D rigidbody;
		protected readonly Animator animator;
		protected readonly SpriteRenderer sprite_renderer;

		/// <summary>
		/// Grabs necessary components
		/// </summary>
		/// <param name="transform">Transform of the game object</param>
		/// <param name="rigidbody">Rigidbody of the game object</param>
		/// <param name="animator">Animator of the game object</param>
		public Motor(Transform transform
			, Rigidbody2D rigidbody
			, Animator animator)
		{
			this.transform = transform;
			this.rigidbody = rigidbody;
			this.animator = animator;
			this.sprite_renderer = this.transform.GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// Moves the transform towards `target_pos` at a specified speed
		/// </summary>
		/// <param name="target_pos">Position to move transform towards</param>
		/// <param name="speed">Speed at which to move the transform</param>
		public abstract void Move(Vector3 target_pos, float speed);
	}
}

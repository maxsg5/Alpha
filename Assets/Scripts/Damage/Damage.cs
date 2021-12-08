// Author: Declan Simkins

using UnityEngine;

/// <summary>
/// Applies damage to a health component and optionally knockback on collision
/// </summary>
public class Damage : MonoBehaviour
{
	[SerializeField] private bool knockback = false;
	[SerializeField] private float knockback_force = 10;
	public float damage = 10.0f;

	/// <summary>
	/// Calls OnTriggerEnter; triggers and collisions are handled the same
	/// </summary>
	/// <param name="other">Collision between the two involved objects</param>
	private void OnCollisionEnter2D(Collision2D other)
	{
		this.OnTriggerEnter2D(other.collider);
	}

	/// <summary>
	/// Applies knockback if it is enabled and damages the other collider if
	/// it has a health script
	/// </summary>
	/// <param name="other">Other collider involved in the collision</param>
	protected virtual void OnTriggerEnter2D(Collider2D other)
    {
	    Health other_health = other.gameObject.GetComponent<Health>();
	    if (other_health) {
		    if (this.knockback) {
			    Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
			    if (rb) {
				    bool flipped = this.GetComponent<SpriteRenderer>().flipX;
				    rb.velocity = new Vector2(0, rb.velocity.y);
				    rb.AddForce(new Vector2(flipped? 2 : -2, 0.5f) * this.knockback_force, ForceMode2D.Impulse);
			    }
		    }
		    
		    other_health.Take_Damage(this.damage);
	    }
    }

	/// <summary>
	/// Deal damage to other collider
	/// </summary>
	/// <param name="other">Collider of other object involved in the collision</param>
	public void doDamage(Collider2D other) {
		Health other_health = other.gameObject.GetComponent<Health>();
		if (other_health != null) {
			other_health.Take_Damage(this.damage);
		}
	}
}

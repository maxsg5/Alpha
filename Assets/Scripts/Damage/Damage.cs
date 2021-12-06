using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	[SerializeField] private bool knockback = false;
	[SerializeField] private float knockback_force = 10;
	public float damage = 10.0f;

	private void OnCollisionEnter2D(Collision2D other)
	{
		this.OnTriggerEnter2D(other.collider);
	}

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

	public void doDamage(Collider2D other) {
		Health other_health = other.gameObject.GetComponent<Health>();
		if (other_health != null) {
			other_health.Take_Damage(this.damage);
		}
	}
}

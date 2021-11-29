using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	public float damage = 10.0f;

	public virtual void OnTriggerEnter2D(Collider2D other)
    {
	    Health other_health = other.gameObject.GetComponent<Health>();
	    if (other_health != null) {
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

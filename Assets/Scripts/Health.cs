using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float max_health = 100.0f;
	public float health = 100.0f;

	public delegate void On_Health_Changed(float new_health);
	public event On_Health_Changed Health_Changed;

	public delegate void On_Death(GameObject obj);
	public event On_Death Death;

	private void Start()
	{
		this.Health_Changed?.Invoke(this.max_health);
	}

	public virtual void Take_Damage(float damage)
	{
		this.health -= damage;
		this.Health_Changed?.Invoke(this.health);
		
		if (this.health <= 0) {
			this.health = 0;
			this.No_Health();
		}
		else if (this.health > this.max_health) {
			this.health = this.max_health;
		}
	}

    protected virtual void No_Health()
    {
		if (this.gameObject.tag == "Player") {
			this.Death?.Invoke(this.gameObject);
			Destroy(this.gameObject);
		} //else if (this.gameObject.tag == "Enemy") {
		// 	this.Death?.Invoke(this.gameObject);
		// 	Destroy(this.gameObject, 1.0f);
		// }
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
	public delegate void On_Toggle(bool state);
	public event On_Toggle Toggled;
	
	private bool active = false;
	public bool Active
	{
		get { return this.active; }
		set
		{
			this.active = value;
			this.Toggled?.Invoke(this.active);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// prompt player to press button to toggle
		if (other.gameObject.CompareTag("Player")) {
			this.Active = !this.active;
		}
	}
}

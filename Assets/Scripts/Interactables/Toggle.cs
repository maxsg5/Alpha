using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Tracks whether the toggle is active or inactive and triggers an event
/// when the state changes
 /// Max Schafer, 2020-11-19: Added audio source to toggle so sound effect is played on interaction
/// </summary>
public class Toggle : MonoBehaviour
{
	public delegate void On_Toggle(bool state);
	public event On_Toggle Toggled;

    private AudioSource audio;

    private bool active = false;

	private void Awake()
	{
		audio = GetComponent<AudioSource>();
	}
	public bool Active
	{
		get => this.active;
		set => this.Set_Active(value);
	}

	/// <summary>
	/// Updates active and invokes the Toggled event.
	/// </summary>
	/// <param name="new_state">The state to be toggled to</param>
	private void Set_Active(bool new_state)
	{
		this.active = new_state;
		this.Toggled?.Invoke(this.active);
	}

	/// <summary>
	/// Toggles the state of the toggle
	/// </summary>
	public void Toggle_Active()
	{
		this.Active = !this.active;
		audio.Play();
	}
}

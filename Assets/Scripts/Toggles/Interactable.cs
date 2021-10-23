using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D), typeof(SphereSensor))]
public class Interactable : MonoBehaviour
{
	[Tooltip("Message to display to the player when standing in proximity of the interactable." +
	         "Will display as \"[prompt] (Press [interact_key])\"")]
	[SerializeField] private string prompt;
	[SerializeField] private Text prompt_text_element;
	[SerializeField] private float interaction_range; // Does nothing until the sensor is turned back on
	[SerializeField] private KeyCode interact_key;	// Should probably be a string referencing an axis or
													// some kind of enum
	
	private SphereSensor sensor;
	private string prompt_suffix;
	private bool player_in_range;

	public UnityEvent on_interact;
	public string Prompt
	{
		get => this.prompt;
		set
		{
			this.prompt = value;
			this.prompt_text_element.text = value + this.prompt_suffix;
		}
	}

	private void Start()
	{
		this.sensor = this.gameObject.GetComponent<SphereSensor>();
		this.sensor.Radius = this.interaction_range; 

		this.prompt_suffix = " (Press [" + (char) this.interact_key + "])";
		this.Prompt = this.prompt;
		this.prompt_text_element.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (this.player_in_range && Input.GetKeyDown(this.interact_key)) {
			this.on_interact.Invoke();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		this.OnTriggerStay2D(other);
		this.Prompt = this.prompt;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Player")) return;

		// Until I can get sensor working right
		this.player_in_range = true;
		this.prompt_text_element.gameObject.SetActive(this.player_in_range);
		
		// When I can get sensor working right
		// this.player_in_range = this.sensor.CanSee(other.gameObject.transform);
		// this.prompt_text_element.gameObject.SetActive(this.player_in_range);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) {
			this.player_in_range = false;
			this.prompt_text_element.gameObject.SetActive(false);
		}
	}
}

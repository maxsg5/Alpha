using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// Allows the triggering of unity events when the player is within
/// interaction range by pressing a specified key. Displays a prompt
/// when the player is in range. 
/// </summary>
[RequireComponent(typeof(Collider2D), typeof(SphereSensor))]
public class Interactable : MonoBehaviour
{
	[Tooltip("Message to display to the player when standing in proximity of the interactable." +
	         "Will display as \"[prompt] (Press [interact_key])\"")]
	[SerializeField] private string prompt;
	[SerializeField] private TMPro.TextMeshProUGUI prompt_text_element; //Note (Max Schafer, 2021-11-17): Changed from Text to TMPro.TextMeshProUGUI. Might want to change back to Text if performance is an issue.
	[SerializeField] private float interaction_range; // Does nothing until the sensor is turned back on
	[SerializeField] private KeyCode interact_key = KeyCode.E;	// Should probably be a string referencing an axis or
																// some kind of enum
	[SerializeField] private bool interact_on_collision = false;

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

	/// <summary>
	/// Grab components, set up initial values, add suffix with interaction key
	/// to the prompt
	/// </summary>
	private void Start()
	{
		this.sensor = this.gameObject.GetComponent<SphereSensor>();
		this.sensor.Radius = this.interaction_range;

		this.prompt_suffix = " (Press [" + (char) this.interact_key + "])";
		this.Prompt = this.prompt;
		this.prompt_text_element.gameObject.SetActive(false);
	}

	/// <summary>
	/// Invoke the on_interact event if the player is in range of the
	/// interactable and the interaction key is pressed
    /// Max Schafer, 2020-11-19: Added the code to remove the prompt and the script after interaction
	/// </summary>
	private void Update()
	{
		if (this.player_in_range && Input.GetKeyDown(this.interact_key)) {
			this.on_interact.Invoke();
			this.prompt_text_element.gameObject.SetActive(false);
			Destroy(GetComponent<Interactable>());
		}
	}

	/// <summary>
	/// Update prompt so that multiple interactables can share one text
	/// element, then use sensor to determine if player is in range and
	/// has line of sight; if these are true, display the interaction prompt 
	/// </summary>
	/// <param name="other">Other collider involved in the collision</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.interact_on_collision) {
			this.on_interact.Invoke();
			Destroy(this);
		}
		
		this.Prompt = this.prompt;
		this.OnTriggerStay2D(other);
	}

	/// <summary>
	/// Determines if player is in range and has line of sight; if these
	/// are true, display the interaction prompt
	/// </summary>
	/// <param name="other">Other collider involved in the collision</param>
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

	/// <summary>
	/// Disable prompt if the player is leaving the collider
	/// </summary>
	/// <param name="other">Other collider involved in the collision</param>
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		
		this.player_in_range = false;
		this.prompt_text_element.gameObject.SetActive(false);
	}

	public void Destroy_Interactable()
	{
		this.prompt_text_element.gameObject.SetActive(false);
		Destroy(this.GetComponent<Interactable>());
	}

	public void Disable_Interactable()
	{
		this.prompt_text_element.gameObject.SetActive(false);
		this.gameObject.GetComponent<Interactable>().enabled = false;
		this.gameObject.GetComponent<Collider2D>().enabled = false;
	}
}

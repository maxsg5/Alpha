// Author: Declan Simkins

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers a UnityEvent in response to all toggles becoming active
/// 'Door' is a poor name really, it's much more general use than that
/// </summary>
public class Door : MonoBehaviour
{
	[SerializeField] private List<Toggle> toggles;
	
    private Interactable interactable;
    
    public UnityEvent on_all_toggles_active;

    /// <summary>
    /// Subscribes to Toggled events
    /// Max Schafer, 2020-11-19: Added the code to initialize the interactable
    /// </summary>
    private void Awake()
	{
		interactable = GetComponent<Interactable>();
		interactable.enabled = false;
		foreach (Toggle toggle in this.toggles) {
			toggle.Toggled += this.Handle_On_Toggle;
		}
	}

	/// <summary>
	/// Unsubscribes from Toggled events
	/// </summary>
	private void OnDestroy()
	{
		foreach (Toggle toggle in this.toggles) {
			toggle.Toggled -= this.Handle_On_Toggle;
		}
	}

	/// <summary>
	/// Handles Toggled event by invoking the UnityEvent methods set in
	/// the editor if all toggles in the the `toggles` list are active
	/// </summary>
	/// <param name="state">State of the toggle that has just changed state</param>
	private void Handle_On_Toggle(bool state)
	{
		if (this.All_Active()) {
			this.on_all_toggles_active.Invoke();
            interactable.enabled = true;
        }
	}

	/// <summary>
	/// Determines whether or not all toggles are active
	/// </summary>
	/// <returns>True if all the toggles are active, false otherwise</returns>
	private bool All_Active()
	{
		bool all_active = true;
		foreach (Toggle toggle in this.toggles) {
			all_active &= toggle.Active;
		}

		return all_active;
	}
}
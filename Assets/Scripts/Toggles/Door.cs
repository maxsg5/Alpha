using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	[SerializeField] private List<Toggle> toggles;
	public UnityEvent on_all_toggles_active;

	private void Awake()
	{
		foreach (Toggle toggle in this.toggles) {
			toggle.Toggled += this.Handle_On_Toggle;
		}
	}

	private void OnDestroy()
	{
		foreach (Toggle toggle in this.toggles) {
			toggle.Toggled -= this.Handle_On_Toggle;
		}
	}

	private void Handle_On_Toggle(bool state)
	{
		if (this.All_Active()) {
			this.on_all_toggles_active.Invoke();
		}
	}

	private bool All_Active()
	{
		bool all_active = true;
		foreach (Toggle toggle in this.toggles) {
			all_active &= toggle.Active;
		}

		return all_active;
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Ammo_Tracker : MonoBehaviour
{
	// TODO: Respond to a "Weapon Switched" event from the player to change the target weapon
	[SerializeField] private Weapon target_weapon;
	[SerializeField] private TextMeshProUGUI ammo_text;

	private void Awake()
	{
		this.target_weapon.Ammo_Changed += this.On_Ammo_Changed;
	}

	private void On_Ammo_Changed(int new_ammo_count)
	{
		string ammo_string = "";
		ammo_string += this.target_weapon.Name + '\n';
		ammo_string += new_ammo_count;
		ammo_string += " / ";
		ammo_string += this.target_weapon.Max_Ammo;
		this.ammo_text.text = ammo_string;
	}
}

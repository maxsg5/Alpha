using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Ammo_Tracker : MonoBehaviour
{
	// TODO: Respond to a "Weapon Switched" event from the player to change the target weapon
	[SerializeField] private Weapon target_weapon;
	[SerializeField] private CharacterController target_character;
	[SerializeField] private TextMeshProUGUI ammo_text;

	private void Awake()
	{
		if (this.target_weapon != null) {
			this.target_weapon.Ammo_Changed += this.On_Ammo_Changed;
		}
		else {
			this.ammo_text.gameObject.SetActive(false);
		}

		this.target_character.Weapon_Changed += this.On_Weapon_Changed;
	}

	private void OnDisable()
	{
		if (this.target_weapon != null) {
			this.target_weapon.Ammo_Changed -= this.On_Ammo_Changed;
		}

		this.target_character.Weapon_Changed -= this.On_Weapon_Changed;
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

	private void On_Weapon_Changed(Weapon new_weapon)
	{
		if (this.target_weapon == null) {
			this.target_weapon = new_weapon;
			this.ammo_text.gameObject.SetActive(true);
		}
		this.Switch_Target_Weapon(new_weapon);
		this.On_Ammo_Changed(this.target_weapon.Current_Ammo);
	}

	private void Switch_Target_Weapon(Weapon new_weapon)
	{
		this.target_weapon.Ammo_Changed -= this.On_Ammo_Changed;
		this.target_weapon = new_weapon;
		this.target_weapon.Ammo_Changed += this.On_Ammo_Changed;
	}
}

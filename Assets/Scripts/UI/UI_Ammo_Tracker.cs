// Author: Declan Simkins

using TMPro;
using UnityEngine;
using Weapons;

namespace UI
{
	/// <summary>
	/// Tracks the specified character's current weapon and the
	/// current ammo for that weapon, updating the specified UI elements
	/// </summary>
	public class UI_Ammo_Tracker : MonoBehaviour
	{
		[SerializeField] private Weapon target_weapon;
		[SerializeField] private CharacterController target_character;
		[SerializeField] private TextMeshProUGUI ammo_text;

		/// <summary>
		/// Subscribe to events, disables display text if there is
		/// no target weapon
		/// </summary>
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

		/// <summary>
		/// Unsubscribe from events
		/// </summary>
		private void OnDisable()
		{
			if (this.target_weapon != null) {
				this.target_weapon.Ammo_Changed -= this.On_Ammo_Changed;
			}

			this.target_character.Weapon_Changed -= this.On_Weapon_Changed;
		}

		/// <summary>
		/// Creates and sets the ammo string
		/// </summary>
		/// <param name="new_ammo_count">New current amount of ammo in target weapon</param>
		private void On_Ammo_Changed(int new_ammo_count)
		{
			string ammo_string = "";
			ammo_string += this.target_weapon.WeaponName + '\n';
			ammo_string += new_ammo_count;
			ammo_string += " / ";
			ammo_string += this.target_weapon.Max_Ammo;
			this.ammo_text.text = ammo_string;
		}

		/// <summary>
		/// Swaps target weapon to new active weapon on the character
		/// </summary>
		/// <param name="new_weapon">Weapon switched to</param>
		private void On_Weapon_Changed(Weapon new_weapon)
		{
			if (this.target_weapon == null) {
				this.target_weapon = new_weapon;
				this.ammo_text.gameObject.SetActive(true);
			}
			this.Switch_Target_Weapon(new_weapon);
			this.On_Ammo_Changed(this.target_weapon.Current_Ammo);
		}

		/// <summary>
		/// Swaps target weapon to new weapon and unsubscribes / subscribes
		/// to events accordingly
		/// </summary>
		/// <param name="new_weapon">Weapon to switch to</param>
		private void Switch_Target_Weapon(Weapon new_weapon)
		{
			this.target_weapon.Ammo_Changed -= this.On_Ammo_Changed;
			this.target_weapon = new_weapon;
			this.target_weapon.Ammo_Changed += this.On_Ammo_Changed;
		}
	}
}

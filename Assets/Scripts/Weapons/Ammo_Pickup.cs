// Author: Declan Simkins

using UnityEngine;
using Weapons;

/// <summary>
/// Specifies an amount and type of ammo to be used as a pickup
/// </summary>
public class Ammo_Pickup : MonoBehaviour
{
	[SerializeField] private Weapon.Ammo ammo_type;
	[SerializeField] private int ammo_amount;

	public Weapon.Ammo Ammo_Type => this.ammo_type;
	public int Ammo_Amount => this.ammo_amount;
}

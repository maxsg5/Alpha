using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Pickup : MonoBehaviour
{
	[SerializeField] private Weapon.Ammo ammo_type;
	[SerializeField] private int ammo_amount;

	public Weapon.Ammo Ammo_Type => this.ammo_type;
	public int Ammo_Amount => this.ammo_amount;
}

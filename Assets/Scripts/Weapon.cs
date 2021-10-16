using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public enum Ammo
	{
		Basic,
		Shotgun,
		Beam,
		Grenade
	}
	
	public delegate void On_Ammo_Changed(int new_amount);
	public event On_Ammo_Changed Ammo_Changed;

	[SerializeField] private int max_ammo, ammo;
	[SerializeField] private GameObject projectile_prefab;
	
	public Weapon.Ammo ammo_type;

	private void Start()
	{
		this.Fire(); // Just for testing!
	}

	public void Fire()
    {
	    if (!this.Has_Ammo()) {
		    this.No_Ammo();
		    return;
	    }

	    this.ammo--;
	    GameObject projectile = Instantiate(this.projectile_prefab, transform);
	    
	    // Prevent collision between newly created projectile and the object that spawned it
	    Collider2D projectile_collider = projectile.GetComponent<Collider2D>();
	    if (projectile_collider == null) {
		    Debug.LogError("Projectile prefab has no collider.");
	    }

	    Collider2D this_collider = this.gameObject.GetComponent<Collider2D>();
	    if (this_collider != null) {
		    Physics2D.IgnoreCollision(projectile_collider, this_collider);
	    }
    }

    private bool Has_Ammo()
    {
	    return this.ammo > 0;
    }

    private void No_Ammo()
    {
	    // Could play an empty mag "click" sound or pop up a UI message or
	    // make the ammo counter flash red or ...
	    Debug.Log("No Ammo!");
    }
}

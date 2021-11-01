using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Single_Shot : Weapon
{
	protected override List<Collider2D> Spawn_Projectile()
	{
		GameObject projectile = Instantiate(
			this.projectile_prefab
			, this.transform.position
			, this.transform.rotation
		);

		Collider2D projectile_collider = projectile.GetComponent<Collider2D>();
		if (projectile_collider == null) {
			Debug.LogError("Projectile prefab has no collider.");
		}

		List<Collider2D> projectiles = new List<Collider2D> {projectile_collider};
		return projectiles;
	}
}

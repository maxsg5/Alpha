// Author: Declan Simkins

using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
	/// <summary>
	/// Weapon which spawns multiple projectiles
	/// </summary>
	public class Weapon_Multi_Shot : Weapon
	{
		[Tooltip("Positions to spawn projectiles at")]
		[SerializeField] private List<GameObject> projectile_spawns;

		protected override List<Collider2D> Spawn_Projectile()
		{
			List<Collider2D> projectile_colliders = new List<Collider2D>();
			foreach (GameObject spawn in this.projectile_spawns) {
				GameObject projectile = Instantiate(
					this.projectile_prefab
					, spawn.transform.position
					, spawn.transform.rotation
				);
				projectile_colliders.Add(projectile.GetComponent<Collider2D>());
			}

			return projectile_colliders;
		}
	}
}

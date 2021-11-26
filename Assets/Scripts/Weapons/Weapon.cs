using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
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
	[SerializeField] private float fire_rate = 1; // shots per second
	[SerializeField] private Collider2D character_collider;
	
	private float fire_delay, last_shot_time;
	private Camera main_camera;

	[SerializeField] protected GameObject projectile_prefab;
	
	public Weapon.Ammo ammo_type;

	public float Fire_Rate
	{
		get => this.fire_rate;
		set
		{
			this.fire_rate = value;
			this.fire_delay = 1 / this.fire_rate;
		}
	}
	
	
	protected abstract List<Collider2D> Spawn_Projectile();

	protected virtual void Start()
	{
		this.fire_delay = 1 / this.fire_rate; // delay between shots
		this.last_shot_time = -10;
		this.main_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

	protected void LateUpdate()
	{
		if (character_collider.tag == "Enemy") {
			this.transform.right = Vector2.right * character_collider.transform.localScale.x;
			
		} else {
			this.Rotate_To_Mouse();
		}
		
	}

	public void Fire()
	{
		if (Time.time - this.last_shot_time < this.fire_delay) {
			return;
		}
		if (!this.Has_Ammo()) {
			this.No_Ammo();
			return;
		}
		this.ammo--;
		this.last_shot_time = Time.time;

		List<Collider2D> projectile_colliders = this.Spawn_Projectile();
		this.Ignore_Projectile_Collisions(projectile_colliders);
	}

	private void Rotate_To_Mouse()
	{
		Vector3 mouse_pos = Input.mousePosition;
		mouse_pos.z = 0;
		Vector3 mouse_world_point = this.main_camera.ScreenToWorldPoint(mouse_pos);
		mouse_world_point.z = 0;

		this.transform.right = mouse_world_point - this.transform.position;
	}

	private void Ignore_Projectile_Collisions(List<Collider2D> projectile_colliders)
	{
		for (int i = 0; i < projectile_colliders.Count; i++) {
			// Ignore collision with object that spawned projectiles
			Physics2D.IgnoreCollision(this.character_collider, projectile_colliders[i]);

			// Ignore collision with other projectiles spawned
			for (int j = i + 1; j < projectile_colliders.Count; j++) {
				Physics2D.IgnoreCollision(projectile_colliders[i], projectile_colliders[j]);
			}
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
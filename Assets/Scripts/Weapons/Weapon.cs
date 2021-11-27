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

	[SerializeField] private string name;
	[SerializeField] private int max_ammo, ammo;
	[SerializeField] private float fire_rate = 1; // shots per second
	[SerializeField] private Collider2D character_collider;
	[SerializeField] private List<Collider2D> ignore_colliders = new List<Collider2D>();
	[SerializeField] private Transform pivot;

	private float aim_angle = 0;
	private float fire_delay, last_shot_time;
	private Camera main_camera;

	//we might want to add public audioClips for each weapon sound and swap them out in the audioSource.
	private AudioSource audio_source; //audio source for shooting sound

	[SerializeField] protected GameObject projectile_prefab;

	public string Name => this.name;
	public int Max_Ammo => this.max_ammo;
	public int Current_Ammo => this.ammo;
	public Weapon.Ammo ammo_type;
	public float Aim_Angle => this.aim_angle;

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
		this.Ammo_Changed?.Invoke(this.ammo);
		this.audio_source = GetComponent<AudioSource>();//audio source for shooting sound
	}

	protected void LateUpdate()
	{
		if (character_collider.tag == "Enemy") {
			this.transform.right = Vector2.right * character_collider.transform.localScale.x;
			
		} else {
			this.Rotate_To_Mouse(this.pivot);
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
		this.Ammo_Changed?.Invoke(this.ammo);

		List<Collider2D> projectile_colliders = this.Spawn_Projectile();
		this.audio_source.Play();//play the shooting sound
		this.Ignore_Projectile_Collisions(projectile_colliders);
	}

	private void Rotate_To_Mouse(Transform centre)
	{
		Vector3 mouse_pos = Input.mousePosition;
		mouse_pos.z = 0;
		Vector3 mouse_world_point = this.main_camera.ScreenToWorldPoint(mouse_pos);
		mouse_world_point.z = 0;
		
		Vector3 aim_direction = mouse_world_point - centre.position;
		this.aim_angle = Mathf.Atan2(aim_direction.y, aim_direction.x) * Mathf.Rad2Deg;
		centre.eulerAngles = new Vector3(0, 0, this.aim_angle);
		
		bool aiming_left = this.aim_angle > 90 || this.aim_angle < -90;
		this.GetComponent<SpriteRenderer>().flipY = aiming_left;
	}

	private void Ignore_Projectile_Collisions(List<Collider2D> projectile_colliders)
	{
		for (int i = 0; i < projectile_colliders.Count; i++) {
			// Ignore collision with other projectiles spawned
			for (int j = i + 1; j < projectile_colliders.Count; j++) {
				Physics2D.IgnoreCollision(projectile_colliders[i], projectile_colliders[j]);
			}
			
			for (int j = i; j < this.ignore_colliders.Count; j++) {
				Physics2D.IgnoreCollision(projectile_colliders[i], this.ignore_colliders[j]);
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

	public void Add_Ammo(int amount)
	{
		this.ammo += amount;
		if (this.ammo > this.max_ammo) {
			this.ammo = this.max_ammo;
		}

		if (this.ammo < 0) {
			this.ammo = 0;
		}
		this.Ammo_Changed?.Invoke(this.ammo);
	}
}
// Author: Declan Simkins

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
	/// <summary>
	/// Base class for different weapon types
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class Weapon : MonoBehaviour
	{
	
		public enum Ammo
		{
			BASIC,
			SHOTGUN,
			BEAM,
			GRENADE,
			ALL
		}
		
		public delegate void On_Ammo_Changed(int new_amount);
		public event On_Ammo_Changed Ammo_Changed; // Event to be invoked when ammo amount changes

		[FormerlySerializedAs("name")] [SerializeField] private string weapon_name;
		[SerializeField] private int max_ammo, ammo;
		[SerializeField] private float fire_rate = 1; // Shots per second
		[SerializeField] private Collider2D character_collider;
		
		[Tooltip("Colliders to be ignored by projectiles created when the weapon is fired")]
		[SerializeField] private List<Collider2D> ignore_colliders = new List<Collider2D>();
		[SerializeField] private Transform pivot;
		[SerializeField] protected GameObject projectile_prefab;

		private float aim_angle = 0;
		private float fire_delay, last_shot_time;
		private Camera main_camera;
		private SpriteRenderer sprite_renderer;

		//we might want to add public audioClips for each weapon sound and swap them out in the audioSource.
		private AudioSource shoot_audio; //audio source for shooting sound
		
		public Weapon.Ammo ammo_type;

		public string WeaponName => this.weapon_name;
		public int Max_Ammo => this.max_ammo;
		public int Current_Ammo => this.ammo;
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
		
		/// <summary>
		/// Spawns the weapon's projectiles, returning all of their colliders
		/// </summary>
		/// <returns>The colliders of all the projectiles spawned</returns>
		protected abstract List<Collider2D> Spawn_Projectile();

		/// <summary>
		/// Sets up fire rate / delay and grabs necessary components
		/// </summary>
		protected virtual void Start()
		{
			this.fire_delay = 1 / this.fire_rate; // delay between shots
			this.last_shot_time = -10;
			
			this.main_camera = Camera.main;
			this.shoot_audio = this.GetComponent<AudioSource>();
			this.sprite_renderer = this.GetComponent<SpriteRenderer>();
			
			this.Ammo_Changed?.Invoke(this.ammo);
		}

		/// <summary>
		/// Rotates this transform to point at the mouse
		/// </summary>
		protected void LateUpdate()
		{
			if (this.character_collider.CompareTag("Enemy")) {
				this.transform.right = Vector2.right * this.character_collider.transform.localScale.x;
			
			} else {
				this.Rotate_To_Mouse(this.pivot);
			}
		}

		/// <summary>
		/// Fires the weapon if possible by instantiating its projectile
		/// and updates the amount of ammo.
		/// Invokes the Ammo_Changed event
		/// </summary>
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
			this.shoot_audio.Play();//play the shooting sound
			this.Ignore_Projectile_Collisions(projectile_colliders);
		}

		/// <summary>
		/// Rotates this transform around a given point to keep
		/// the weapon pointed at the mouse
		/// </summary>
		/// <param name="centre">Point around which to rotate</param>
		private void Rotate_To_Mouse(Transform centre)
		{
			this.aim_angle = Utilities.Mouse.Angle_To_Mouse(centre.position);
			centre.eulerAngles = new Vector3(0, 0, this.aim_angle);
		
			bool aiming_left = this.aim_angle > 90 || this.aim_angle < -90;
			this.sprite_renderer.flipY = aiming_left;
		}

		/// <summary>
		/// Tells the physics engine to ignore collisions between projectiles
		/// </summary>
		/// 
		/// <param name="projectile_colliders">
		/// All projectile colliders created when firing the weapon
		/// </param>
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

		/// <summary>
		/// Determines whether the weapon has ammo or not
		/// </summary>
		/// <returns>True if the weapon has ammo, false otherwise</returns>
		private bool Has_Ammo()
		{
			return this.ammo > 0;
		}

		/// <summary>
		/// Method to be called when the weapon has no ammo but
		/// is trying to fire
		/// </summary>
		private void No_Ammo()
		{
			// Could play an empty mag "click" sound or pop up a UI message or
			// make the ammo counter flash red or ...
			Debug.Log("No Ammo!");
		}

		/// <summary>
		/// Updates amount of ammo the weapon has
		/// available, capping at the max ammo.
		/// Invokes the Ammo_Changed event
		/// </summary>
		/// <param name="amount">Amount of ammo to be added (can be negative)</param>
		public void Add_Ammo(int amount)
		{
			Debug.Log("[Weapon] Adding " + amount + " ammo to " + this.weapon_name);
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
}
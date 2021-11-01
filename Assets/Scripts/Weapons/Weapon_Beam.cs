using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Beam : Weapon
{
	[SerializeField] private float beam_duration = 3.0f;
	[Tooltip("Multiplies the beam duration by this value to set fire rate.")]
	[SerializeField] private float fire_rate_mod = 2.0f;

	[SerializeField] private Transform beam_origin;

	protected override void Start()
	{
		base.Start();
		this.Fire_Rate = 1 / (this.beam_duration * this.fire_rate_mod);
	}

	protected override List<Collider2D> Spawn_Projectile()
	{
		GameObject beam = Projectile_Beam.Create_Beam(
			this.projectile_prefab
			, this.gameObject.transform
		);
		EdgeCollider2D beam_collider = beam.GetComponent<EdgeCollider2D>();

		Destroy(beam, this.beam_duration);
		return new List<Collider2D>() {beam_collider};
	}
}

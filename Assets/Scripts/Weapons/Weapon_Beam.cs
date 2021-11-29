using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Beam : Weapon
{
	[SerializeField] private float beam_duration = 3.0f;
	[Tooltip("Multiplies the beam duration by this value to set fire rate.")]
	[SerializeField] private float fire_rate_mod = 2.0f;

	[SerializeField] private Transform beam_origin;
	private AudioSource audio_source;

	// TODO (Declan Simkins): Collides with triggers but should pass through them
	
	protected override void Start()
	{
		base.Start();
		this.audio_source = this.GetComponent<AudioSource>();
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
		StartCoroutine(Kill_Beam_Sound(this.beam_duration)); //Turn off the sound after the beam duration. Added by Max Schafer - 2021-11-28
		return new List<Collider2D>() {beam_collider};
	}

	/// <summary>
    ///  Stops the beam sound over the duration of the beam.
    /// </summary>
    /// <param name="duration">time to kill the sound</param>
    /// <returns>Waits for duration</returns>
    /// Author: Max Schafer
    /// Date: 2021-11-28
	private IEnumerator Kill_Beam_Sound(float duration)
	{
		yield return new WaitForSeconds(duration);
		this.audio_source.Stop();
	}
	
}

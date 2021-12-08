// Author: Declan Simkins

using UnityEngine;

/// <summary>
/// Damages on collision every 'tick' seconds
/// </summary>
public class Damage_Continuous : Damage
{
	[Tooltip("How often (in seconds) the damage should be applied.")]
	[SerializeField] private float tick_rate = 0;

	private float last_tick;
	
	protected override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		this.last_tick = Time.time;
	}

	/// <summary>
	/// Continuously damages the target according to the tick_rate
	/// </summary>
	/// <param name="other">Collider of the other game object involved in the collision</param>
	public void OnTriggerStay2D(Collider2D other)
	{
		if (!(Time.time > this.last_tick + this.tick_rate)) {
			return;
		}
		this.OnTriggerEnter2D(other);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Pass_Through : Projectile
{
	[SerializeField] private int max_pass_throughs = 1;
	private int pass_throughs = 0;
	
	protected override void OnCollisionEnter2D(Collision2D other)
	{
		if (this.pass_throughs >= this.max_pass_throughs) {
			Destroy(this.gameObject);
		}

		this.pass_throughs++;
	}
}

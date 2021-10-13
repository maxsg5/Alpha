using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSensor : Sensor
{
	public float radius;
	private float radius2;

	void Start()
	{
		SetRadius(radius);
	}

	public void SetRadius(float r)
	{
		radius = r;
		radius2 = r * r;
	}

	public override bool CanSee(Transform target)
	{
		Vector3 delta = target.position - transform.position;

		if (delta.sqrMagnitude <= radius2)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, delta, out hit, radius))
				return hit.transform == target;
		}
		return false;
	}
}
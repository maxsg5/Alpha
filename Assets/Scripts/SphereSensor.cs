using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check to see if an object can see a target in a circle around it
/// </summary>
/// 
/// Author: Josh Coss 	(JC)
/// 
/// Variables
/// radius			Radius of circle
/// radius2			Radius squared
public class SphereSensor : Sensor
{
	public float radius;
	public float radius2;

	/// <summary>
	/// Sets the radius of the circle
	/// </summary>
	/// 
	/// Date		Author		Description
	/// 2021-10-14	JC			Initial testing
	void Start()
	{
		SetRadius(radius);
	}

	/// <summary>
	/// Sets the radius of the circle and calculates radius2
	/// </summary>
	/// <param name="r">radius, float</param>
	/// 
	/// Date		Author		Description
	/// 2021-10-14	JC			Initial testing
	public void SetRadius(float r)
	{
		radius = r;
		radius2 = r * r;
	}

	/// <summary>
	/// Check to see if the sensor can see the target, returns true if so
	/// </summary>
	/// <param name="target">Transform of target</param>
	/// <returns>bool</returns>
	/// 
	/// Date		Author		Description
	/// 2021-10-14	JC			Initial testing
	/// 2021-10-14	JC			Updated for 2D
	public override bool CanSee(Transform target)
	{
		Vector3 delta = target.position - transform.position;

		if (delta.sqrMagnitude <= radius2)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, delta, radius);
			if (hit.transform == target)
			{
				return true;
			}
		}
		return false;
	}
}
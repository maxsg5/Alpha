using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks to see if the parent object can see a target in a sphere around it
/// </summary>
/// 
/// Author: Josh Coss	(JC)
/// 
/// Variables
/// radius			float radius
/// radius2			float radius^2
public class SphereSensor : Sensor
{
	public float radius;
	public float radius2;

	/// <summary>
	/// Set the radius
	/// </summary>
	/// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
	void Start()
	{
		SetRadius(radius);
	}

	/// <summary>
	/// Set the radius and radius2
	/// </summary>
	/// <param name="r">float radius</param>
	/// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
	public void SetRadius(float r)
	{
		radius = r;
		radius2 = r * r;
	}

	/// <summary>
	/// Check to see if the sensor can see the target
	/// </summary>
	/// <param name="target">Target transform coordinates</param>
	/// <returns>bool true or false</returns>
	/// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
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

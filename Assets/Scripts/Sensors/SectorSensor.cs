using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check to see if an object can see a target in a cone in front of it
/// </summary>
/// 
/// Author: Josh Coss     (JC)
/// 
/// Variables
/// angle           angle in degrees
/// minCosine       cosine of the angle
public class SectorSensor : SphereSensor
{
    public float angle;
    private float minCosine;

    // Note (Declan Simkins): Changed Start to override and call the
    // base class's Start method
    /// <summary>
    /// Sets the radius and angle of view
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    protected override void Start()
    {
	    base.Start();
	    SetAngle(angle);
    }

    /// <summary>
    /// sets the angle of view, converting degrees to radians
    /// </summary>
    /// <param name="degrees">float degrees</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void SetAngle(float degrees) 
    {
        angle = degrees * Mathf.Deg2Rad;
        minCosine = Mathf.Cos(angle);
    }

    /// <summary>
	/// Check to see if the sensor can see the target
	/// </summary>
	/// <param name="target">Target transform coordinates</param>
	/// <returns>bool true or false</returns>
	/// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Updated for 2D
    public override bool CanSee(Transform target) {
        Vector3 heading = target.position - gameObject.transform.position;

        if (heading.sqrMagnitude <= Radius2 && Vector3.Dot(heading.normalized, transform.forward) >= minCosine)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, heading, out hit, Radius))
                return hit.transform == target;
        }
        return false;
    }

}

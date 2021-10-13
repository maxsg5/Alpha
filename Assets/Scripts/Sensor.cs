using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sensor parent class
/// </summary>
/// 
/// Author: Josh Coss       (JC)
public abstract class Sensor : MonoBehaviour
{
    /// <summary>
	/// Check to see if the sensor can see the target
	/// </summary>
	/// <param name="target">Target transform coordinates</param>
	/// <returns>bool true or false</returns>
	/// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public abstract bool CanSee(Transform target);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores an array of control (Vector3) points and calculates the interpolated
/// position
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// Variables
/// control         List of Vector3 control points
/// closed          True if path is closed, false if not
/// length          Number of points in the control path
/// lengthM1        Length minus 1
public abstract class Interpolator
{
    public Vector3[] control;
    public bool closed;
    public int length;

    protected int lengthM1;

    /// <summary>
    /// Sets the control points and closed value based on a list of Vector3 points
    /// </summary>
    /// <param name="points">List of Vector3 points</param>
    /// <param name="closed">Closed value</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public Interpolator (Vector3[] points, bool closed) {
        SetControl(points);
        SetClosed(closed);
    }

    /// <summary>
    /// Sets the control points and closed value based on a list of Transform points
    /// </summary>
    /// <param name="points">List of Transform points</param>
    /// <param name="closed">Closed value</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public Interpolator (Transform[] points, bool closed) {
        SetControl (points);
        SetClosed (closed);
    }

    /// <summary>
    /// Takes in a list of vector3 points and sets them as the list of control points
    /// </summary>
    /// <param name="points">List of Vector3 points</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void SetControl (Vector3[] points) {
        control = points;
    }

    /// <summary>
    /// Takes in a list of Transform points, calculates lengthM1, and puts each transform
    /// point into a list of vector3 objects. Then sets as list of control points.
    /// </summary>
    /// <param name="points">List of Transform points</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void SetControl (Transform[] points) {
        length = points.Length;
        lengthM1 = length - 1;

        control = new Vector3[length];
        for (int i=0;i < length; i++)
            control[i] = points[i].position;
    }

    /// <summary>
    /// Takes in a bool and sets it as the closed value
    /// </summary>
    /// <param name="newClosed">Bool</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void SetClosed (bool newClosed) {
        closed = newClosed;
    }

    public abstract Vector3 Evaluate (float u);
    public abstract Vector3 Heading (float u);
}

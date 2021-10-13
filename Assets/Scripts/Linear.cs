using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Linear pathing that allows an object to move along a list of control
/// points in a straight line
/// </summary>
/// 
/// Author: Josh Coss   (JC)
public class Linear: Interpolator {

    /// <summary>
    /// Gets and sets a list of Vector3 points to the control point list, and
    /// gets and sets the closed variable
    /// </summary>
    /// <param name="pts">List of Vector 3 objects</param>
    /// <param name="closed">Closed bool</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public Linear (Vector3[] pts, bool closed=false):base (pts, closed) {
        SetControl (pts);
        SetClosed (closed);
    }

    /// <summary>
    /// Gets and sets a list of Transform points to the control point list, and
    /// gets and sets the closed variable
    /// </summary>
    /// <param name="pts">List of Transform points</param>
    /// <param name="closed">Closed bool</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public Linear (Transform[] pts, bool closed=false):base (pts, closed) {
        SetControl (pts);
        SetClosed (closed);
    }

    /// <summary>
    /// Calculates and returns location on the interpolation curve
    /// </summary>
    /// <param name="u">A float point somewhere from zero to the length of the
    ///                 control point array</param>
    /// <returns>Vector3 point on interpolation curve</returns>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public override Vector3 Evaluate (float u) {
        float dU;
        int startIndex, finishIndex;

        startIndex = Mathf.FloorToInt(u);
        CalculateFactors (u, closed,
                        out dU, ref startIndex, out finishIndex);
        return Vector3.Lerp (control[startIndex], control[finishIndex], dU);
    }

    /// <summary>
    /// Makes the object face the direction of travel along the interpolation curve
    /// </summary>
    /// <param name="u">The point in length along the length of the control point array</param>
    /// <returns>Vector3 of the direction to face</returns>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public override Vector3 Heading (float u) {
        float dU;
        int startIndex, finishIndex;

        startIndex = Mathf.FloorToInt(u);
        CalculateFactors (u, closed,
                        out dU, ref startIndex, out finishIndex);
        return (control[finishIndex] - control[startIndex]).normalized;
    }

    /// <summary>
    /// Calculates dU and finishIndex
    /// </summary>
    /// <param name="u">The point in length along the length of the control point array</param>
    /// <param name="closed">bool stating whether path is closed</param>
    /// <param name="dU"></param>
    /// <param name="startIndex">int</param>
    /// <param name="finishIndex">int</param>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    private void CalculateFactors (float u, bool closed,
                out float dU, ref int startIndex, out int finishIndex) {
        startIndex = Mathf.FloorToInt (u);
        dU = u - startIndex;
        finishIndex = startIndex + 1;
       
       if (startIndex == lengthM1) {
           if (!closed) {
               finishIndex = startIndex;
               startIndex = startIndex - 1;
               dU += 1.0f;
           } else {
               finishIndex = 0;
           }
       }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the object to move along a set path using control points
/// set in the world
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// Variables
/// cPoints         List of Transform control points
/// closedLoop      Bool (true if closed)
/// interpolator    Interpolator object
/// turnSpeed       Enemy turn speed
/// moveSpeed       Enemy move speed
/// u               A float point somewhere from zero to the length of the control point array
/// physics         Rigidbody object
public class PathMove : MonoBehaviour
{
    public Transform[] cPoints;
    public bool closedLoop;

    private Interpolator interpolator;
    private float turnSpeed, moveSpeed, u;
    private Rigidbody physics;
    
    /// <summary>
    /// Gets the rigidbody, interpolator, and sets u to 0
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    void Start () {
        physics = GetComponent<Rigidbody>();
        interpolator = new Linear(cPoints, closedLoop);
        u = 0.0f;
    }

    /// <summary>
    /// Updates the movement of the object the script is attached to
    /// along the control points
    /// </summary>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    void Update() {
        u += Time.deltaTime * moveSpeed;
        
        if (u >= interpolator.length)
        {
            if (interpolator.closed)
                u -= interpolator.length;
            else
                u = interpolator.length;
        }

        Vector3 targetDirection = interpolator.Heading (u);

        transform.position = interpolator.Evaluate (u);

        float singleStep = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, 
				targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        physics.velocity = transform.forward * turnSpeed;
    }
    
    /// <summary>
    /// Sets the turns speed
    /// </summary>
    /// <param name="tSpeed">float turn speed</param>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void setTurnSpeed(float tSpeed) {
        turnSpeed = tSpeed;
    }

    /// <summary>
    /// Sets the movement speed
    /// </summary>
    /// <param name="mSpeed">float movement speed</param>
    ///  
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void setMoveSpeed(float mSpeed) {
        moveSpeed = mSpeed;
    }
}

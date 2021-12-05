using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to the camera in the main menu scene. It allows the camera to be panned 
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-05
public class CameraPan : MonoBehaviour
{
    #region Private Variables
    private float panSpeed = 1f; // The speed at which the camera pans
    private float panRightPoint = 30f; // The point at which the camera pans to the right
    private float panLeftPoint = 12f; // The point at which the camera pans to the left
    private bool movingRight = true; // Determines if the camera is moving right
    #endregion
   
    #region Methods

    /// <summary>
    /// Pans the camera to the right until it reaches the right point 
    /// and then pans to the left until it reaches the left point.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    void Update()
    {
        // If the camera is moving right
        if(movingRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed);
        }
        //otherwise move the camera left.
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * panSpeed);
        }
        // Check if the camera needs to invert direction.
        if(transform.position.x >= panRightPoint)
        {
            movingRight = false;
        }
        else if(transform.position.x <= panLeftPoint)
        {
            movingRight = true;
        }
    }

    #endregion
}

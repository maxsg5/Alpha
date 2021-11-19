using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SlidingDoor is a class that handles the sliding door mechanic for the Hangar door.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-11-19
/// Description: Initial testing
public class SlidingDoor : MonoBehaviour
{
    public bool isOpen = false; // is the door open?
    

    /// <summary>
    /// Update checks if the door is open and if it is, it will move the door down.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
    void Update()
    {
        // if the door is open, move the door down
        if(isOpen)
        {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
            //if the door is open and it is at the bottom, destroy the door object.
            if(transform.position.y < -40)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

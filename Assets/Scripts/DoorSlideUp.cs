using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Slides the door up at the end of the level 
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-01
public class DoorSlideUp : MonoBehaviour
{
    public bool slideUp = false; // if the door should slide up

    /// <summary>
    /// Responsible for sliding the door up if the player has passed a trigger.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    void Update()
    {
        // if the door should slide up
        if (slideUp)
        {
            // move the door up
            transform.Translate(Vector3.right * 5 * Time.deltaTime );
            
            // if the door is at the top stop sliding.
            if(transform.position.y >= -7)
            {
                slideUp = false;
            }
        }
        
    }

    
}

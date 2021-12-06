using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to the FinalDoor object. and opens when the player defeats the boss.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-06
public class FinalDoor : MonoBehaviour
{

    public GameObject boss; // reference to the boss object
    

    /// <summary>
    /// Check if the boss's health is at 0. If it is, open the door.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-06
    void Update()
    {
        if(boss.GetComponent<Health>().health <= 0)
        {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
            //if the door is open and it is at the bottom, destroy the door object.
            if(transform.position.y < -60)
            {
                Destroy(gameObject);
            }
        }
    }
}

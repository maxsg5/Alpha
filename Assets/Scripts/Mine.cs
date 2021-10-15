using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Responsible for detonating the mine if the character lands on it.
///</summary>
///
///Author: Braden Simmons (BS)
///
///sensor   The sensor that detects the object
///
///player   The character that the mine is detecting
///
///strength How much health the mine depletes from the character
///
///detonated    Determines if the mine has been detonated or not

public class Mine : MonoBehaviour
{
    private SphereSensor sensor;
    public Transform player;
    public float strength = 2f;
    public bool detonated = false;

    ///<summary>
    ///Initialize the sensor
    ///</summary>
    ///Date         Author      Description
    ///2021-10-13   BS          Intializes sensor
    void Start()
    {
        sensor = GetComponent<SphereSensor>();
    }

    ///<summary>
    ///The sensor looks for the layer in order to detonate the mine. If the player is detected,
    ///the health of the player is depleted.
    ///</summary>
    ///Date         Author      Description
    ///2021-10-13   BS          Detect the player and detonate
    void Update()
    {
        if(sensor.CanSee(player)){
            if(detonated == false){
                //Deplete characters health by strength
                //player.depleteHealth()
                //Explode animation
            }
        } 
    }
}

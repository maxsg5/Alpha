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
    public Transform target;
    public Character player;
    public float strength = 2f;
    public bool detonated = false;

    ///<summary>
    ///Initialize the sensor
    ///</summary>
    ///Date         Author      Description
    ///2021-10-13   BS          Intializes sensor
    ///2021-10-27   BS          Initialize player
    void Start()
    {
        sensor = GetComponent<SphereSensor>();
        player = GetComponent<Character>();
    }

    ///<summary>
    ///The sensor looks for the layer in order to detonate the mine. If the player is detected,
    ///the health of the player is depleted.
    ///</summary>
    ///Date         Author      Description
    ///2021-10-13   BS          Detect the player and detonate
    ///2021-10-27   BS          Remove the health of the player
    void Update()
    {
        if(sensor.CanSee(target)){
            player.Take_Damage(strength);
            Destroy(gameObject);
        } 
    }
}

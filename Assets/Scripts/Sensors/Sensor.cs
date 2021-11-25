using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Sensor Base
///Parent Abstract class for the sensors
///</summary>
///
///Author: Braden Simmons (BS)

public abstract class Sensor : MonoBehaviour
{
    ///<summary>
    ///Determines if the target is in the sensors radius
    ///</summary>
    ///
    ///target   The location of the target
    ///
    ///Date         Author      Description
    ///2021-10-01   BS          Determine if the sensor can see the target
    public abstract bool CanSee(Transform target);
}

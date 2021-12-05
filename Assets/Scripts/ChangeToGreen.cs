using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change the signal lights to green once the player has interacted with a coresponding switch.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-11-19
/// Description: Initial testing
public class ChangeToGreen : MonoBehaviour
{
    private Light light; // The light that will be turned green.
    
    /// <summary>
    /// Initialize the Light component.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
    void Start()
    {
        light = GetComponent<Light>();
    }

    /// <summary>
    /// Changes light colour to green.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
    public void ChangeColor()
    {
        light.color = Color.green;
    }
}

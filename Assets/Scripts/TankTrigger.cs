using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script handles the trigger for starting the tank cut scene.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-05
public class TankTrigger : MonoBehaviour
{
    #region Public Variables
    public GameObject tank; // Reference to the Tank gameobject.
    #endregion
  
    #region Private Variables
    private AudioSource audioSource; // Reference to an audio source attached to the trigger to play the tank sound.
    #endregion
  
    #region Methods
    /// <summary>
    /// Initialize the audio source reference.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// If the player enters the trigger, we start the tank cut scene.
    /// </summary>
    /// <param name="other">Collider2D</param>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player enters the trigger, we start the tank cut scene.
        if (other.gameObject.tag == "Player")
        {
            //disable player control
            other.gameObject.GetComponent<CharacterController>().disableMovement = true;
            //tell the tank to start moving
            tank.GetComponent<Tank>().isMoving = true;
            //play the tank sound
            audioSource.Play();
            //Destroy the trigger after the tank sound has finished playing. This avoids the player from being able to enter the trigger again.
            Destroy(gameObject, audioSource.clip.length);
        }
    }
    #endregion
}

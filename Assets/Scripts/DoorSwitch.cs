using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for the master door switch which is used to open the hangar door once all the other switches are activated.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-11-19
/// Description: Initial testing
[RequireComponent(typeof(Animator))]
public class DoorSwitch : MonoBehaviour
{
    #region Public Variables
    public SlidingDoor door; // The door to control
    #endregion
    
    #region Private Variables
    private Animator animator; // Reference to the animator component.
    private AudioSource audio; // AudioSource component
    #endregion
    
    #region Methods
    /// <summary>
    /// Initialization of the Animator and AudioSource components.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        
    }

    /// <summary>
    /// Set animation to On
    /// Set door to open
    /// Play sound effect.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
    public void OpenDoor()
    {
        
        animator.SetBool("On", true);
        door.isOpen = true;
        PlaySoundInterval(0, 3.5f);
    }

    /// <summary>
    /// Plays a sound effect at a given interval.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-11-19
    /// Description: Initial testing
     void PlaySoundInterval(float fromSeconds, float toSeconds)
    {
        audio.time = fromSeconds;
        audio.Play();
        audio.SetScheduledEndTime(AudioSettings.dspTime+(toSeconds-fromSeconds));
    }

    #endregion
}

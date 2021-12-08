using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script plays the reload sound when the player reloads their weapon.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-07
public class Reload : MonoBehaviour
{
    private AudioSource audioSource; // The audio source for the reload sound.

    /// <summary>
    /// initialize the audio source.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-07
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the reload sound.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-07
    public void PlayReloadSound()
    {
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used to trigger audio swaps. once the Player enters the trigger, the audio will swap.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-01
public class AudioSwap : MonoBehaviour
{
    public AudioClip newTrack; // the new audio track to play

    /// <summary>
    /// Once the player enters the trigger area, we swap the audio track.
    /// </summary>
    /// <param name="other">Collider2D</param>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.SwapTrack(newTrack);
        }
    }
}

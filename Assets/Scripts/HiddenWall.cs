using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Trigger for the hidden wall to slide up once the player has entered the trigger.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-01
public class HiddenWall : MonoBehaviour
{
    public GameObject wall; // The wall that slides upwards
     private AudioSource audio; // The audio source for the wall sliding sound

    /// <summary>
    /// Initialize the audio source
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    /// <summary>
    /// If the player enters the trigger, play the sliding sound and slide the wall up.
    /// Destroy the collider so we can't trigger the slide any more.
    /// </summary>
    /// <param name="other">Collider2D</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            wall.GetComponent<DoorSlideUp>().slideUp = true;
            PlaySoundInterval(0, 3.5f);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
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
}

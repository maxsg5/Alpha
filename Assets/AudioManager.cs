using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Audio manager is responsible for playing the background music and transitioning between songs.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-01
public class AudioManager : MonoBehaviour
{
    public AudioClip main_theme; // The main theme of the game
    public static AudioManager instance; // Singleton since we only want one instance at a time.
    private AudioSource audioSource1, audioSource2; // Audio sources for the two songs.
    private bool isPlayingClip1, isPlayingClip2; // Bools for checking if the clips are playing.
    

    /// <summary>
    /// Awake is responsible for initializing the singleton.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    /// <summary>
    /// Initializes the audio sources and sets clip 1 to playing.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource1.loop = true;
        audioSource2.loop = true;
        isPlayingClip1 = true;
        isPlayingClip2 = false;
        //call SwapTrack with main theme to start playing the main theme.
        SwapTrack(main_theme);
    }

    /// <summary>
    /// Responsible for swapping the audio clip in the audio sources. using a fade out and fade in.
    /// </summary>
    /// <param name="newTrack">Audio Clip you want to play next.</param>
    public void SwapTrack(AudioClip newTrack)
    {
        //we need to stop all ongoing fade outs if we are swapping tracks.
        StopAllCoroutines();
        // Start fade out coroutine.
        StartCoroutine(FadeOut(newTrack)); 
        //if we are not playing clip1, then we are playing clip2.
        if (!isPlayingClip1)
        {
            audioSource2.clip = newTrack;
            audioSource2.Play();
            audioSource1.Stop();
        }
        else
        {
            audioSource1.clip = newTrack;
            audioSource1.Play();
            audioSource2.Stop();
        }
        isPlayingClip1 = !isPlayingClip1; //flip the bool.
    }

    /// <summary>
    /// Fades back to the main theme.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    public void ReturnToMainTheme()
    {
       SwapTrack(main_theme);
    }

    /// <summary>
    /// Fades out the current track and fades in the new track.
    /// lerp is used to lower the volume of the current track and increase the volume of the new track.
    /// </summary>
    /// <param name="newTrack">AudioClip you want to fade in</param>
    /// <returns>null</returns>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    private IEnumerator FadeOut(AudioClip newTrack)
    {
        float fadeTime = 5.25f; // time it takes to fade out.
        float timeElasped = 0.0f; // time elapsed.
        if (!isPlayingClip1)
        {
            audioSource2.clip = newTrack;
            audioSource2.Play();
            while(timeElasped < fadeTime)
            {
                audioSource2.volume = Mathf.Lerp(0,1,timeElasped/fadeTime);
                audioSource1.volume = Mathf.Lerp(1,0,timeElasped/fadeTime);
                timeElasped += Time.deltaTime;
                yield return null;
            }
            audioSource1.Stop();
        }
        else
        {
            audioSource1.clip = newTrack;
            audioSource1.Play();
            while(timeElasped < fadeTime)
            {
                audioSource1.volume = Mathf.Lerp(0,1,timeElasped/fadeTime);
                audioSource2.volume = Mathf.Lerp(1,0,timeElasped/fadeTime);
                timeElasped += Time.deltaTime;
                yield return null;
            }
            audioSource2.Stop();
        }
        isPlayingClip1 = !isPlayingClip1;
    }
}

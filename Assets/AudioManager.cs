using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip main_theme;
    public static AudioManager instance; // Singleton since we only want one instance at a time.
    private AudioSource audioSource1, audioSource2;
    private bool isPlayingClip1, isPlayingClip2;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        isPlayingClip1 = true;
        isPlayingClip2 = false;
        SwapTrack(main_theme);
    }

    public void SwapTrack(AudioClip newTrack)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(newTrack));
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
        isPlayingClip1 = !isPlayingClip1;
    }

    public void ReturnToMainTheme()
    {
       SwapTrack(main_theme);
    }

    private IEnumerator FadeOut(AudioClip newTrack)
    {
        float fadeTime = 5.25f;
        float timeElasped = 0.0f;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwap : MonoBehaviour
{
    public AudioClip newTrack;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AudioSwap");
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.SwapTrack(newTrack);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTrigger : MonoBehaviour
{

    public GameObject tank;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// If the player enters the trigger,
    /// </summary>
    /// <param name="other">Collider2D</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterController>().disableMovement = true;
            tank.GetComponent<Tank>().isMoving = true;
            audioSource.Play();
        }
    }
}

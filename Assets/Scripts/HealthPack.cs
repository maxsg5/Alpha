using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Health pack heals the player when collided with.
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-01
public class HealthPack : MonoBehaviour
{
    public AudioSource healSound; // Sound to play when health pack is picked up.

    /// <summary>
    /// When the player collides with the health pack, the player's health is restored.
    /// </summary>
    /// <param name="collision">Collision2D</param>
    /// Author: Max Schafer
    /// Date: 2021-12-01
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Add_Health(100);
            healSound.Play();
            Destroy(gameObject);
        }
    }
}

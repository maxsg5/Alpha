using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Melee attack trigger attached to a collision box on the enemy.
/// </summary>
/// 
/// Author: Josh Coss (JC)
/// 
/// Variable:
/// inRange              Whether or not the player is in range of the enemy.
/// player_collider      The player's collider.
public class AttackTrigger : MonoBehaviour
{
    public bool inRange;
    public Collider2D player_collider;

    /// <summary>
    /// Returns whether or not the player is in range of the enemy.
    /// </summary>
    /// <returns>inRange - bool</returns>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public bool isInRange()
    {
        return inRange;
    }

    /// <summary>
    /// Returns the player's collider.
    /// </summary>
    /// <returns>player_collider</returns>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    public Collider2D getPlayerCollider()
    {
        return player_collider;
    }

    /// <summary>
    /// Called when the player enters the trigger and sets player_collider to the player's collider.
    /// Also sets inRange to true.
    /// </summary>
    /// <param name="collision">Collider of object inside trigger</param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player_collider = collision;
            inRange = true;
        }
    }

    /// <summary>
    /// Called when the player exits the trigger and sets inRange to false
    /// </summary>
    /// <param name="collision">Collider of object inside trigger</param>
    /// 
    /// Date        Author      Description
    /// 2021-12-08  JC          Initial Testing
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
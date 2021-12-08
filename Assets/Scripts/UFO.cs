using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Responsible for moving the UFO and destorying the character
///</summary>
///
///Author: Braden Simmons (BS)
///
///OnBoard      Determines if the character collided with the ship

public class UFO : MonoBehaviour
{
    public GameObject gameOverMenu; //The game over menu to be displayed
    private bool onBoard = false;

    ///<summary>
    ///Move the ship
    ///</summary>
    ///Date         Author      Description
    ///2021-11-26   BS          Move the ship
    void Update()
    {
        if(onBoard){
            transform.Translate(Vector3.up * 10 * Time.deltaTime);
            gameOverMenu.SetActive(true);
            if(transform.position.y > 40){
                Destroy(gameObject);
            }
        }
    }

    ///<summary>
    ///Destroy the character
    ///</summary>
    ///Date         Author      Description
    ///2021-11-26   BS          Destroy character
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Destroy(collision.gameObject);
            onBoard = true;
        }
    }
}
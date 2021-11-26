using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    private bool onBoard = false;

    // Update is called once per frame
    void Update()
    {
        if(onBoard){
            transform.Translate(Vector3.up * 10 * Time.deltaTime);
            if(transform.position.y > 40){
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Destroy(collision.gameObject);
            onBoard = true;
        }
    }
}

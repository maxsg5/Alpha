using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{

    public GameObject boss;
    

    // Update is called once per frame
    void Update()
    {
        if(boss.GetComponent<Health>().health <= 0)
        {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
            //if the door is open and it is at the bottom, destroy the door object.
            if(transform.position.y < -60)
            {
                Destroy(gameObject);
            }
        }
    }
}

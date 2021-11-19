using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
        {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
            if(transform.position.y < -40)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private float panSpeed = 1f;
    private float panRightPoint = 30f;
    private float panLeftPoint = 12f;
    private bool movingRight = true;

    // Update is called once per frame
    void Update()
    {
        if(movingRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * panSpeed);
        }

        if(transform.position.x >= panRightPoint)
        {
            movingRight = false;
        }
        else if(transform.position.x <= panLeftPoint)
        {
            movingRight = true;
        }
        
    }
}

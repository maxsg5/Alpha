using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToGreen : MonoBehaviour
{
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    public void ChangeColor()
    {
        light.color = Color.green;
    }
}

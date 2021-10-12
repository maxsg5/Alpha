using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrooper : Enemy
{

    public float turnSpeed = 0.0f;
    public float moveSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PathMove>();
        sensor = transform.Find("Sensor").GetComponent<SectorSensor>();
        physics = GetComponent<Rigidbody>();

        movement.setTurnSpeed(turnSpeed);
        movement.setMoveSpeed(moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (sensor.CanSee(target)) {
            Debug.Log("Hello Sphere");
        }
    }
}

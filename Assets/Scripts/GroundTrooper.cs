using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrooper : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
        interpolator = new Linear(cPoints, closedLoop);
    }

    // Update is called once per frame
    void Update()
    {
        u += Time.deltaTime * U_SPEED;
        
        if (u >= interpolator.length)
        {
            if (interpolator.closed)
                u -= interpolator.length;
            else
                u = interpolator.length;
        }

        Vector3 targetDirection = interpolator.Heading (u);
        transform.position = interpolator.Evaluate (u);
        physics.velocity = transform.forward * speed;
    }
}

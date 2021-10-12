using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : MonoBehaviour
{
    public Transform[] cPoints;
    public bool closedLoop;

    private Interpolator interpolator;
    private float turnSpeed, moveSpeed, u;
    private Rigidbody physics;
    private Material myMaterial;

    public void setTurnSpeed(float tSpeed) {
        turnSpeed = tSpeed;
    }

    public void setMoveSpeed(float mSpeed) {
        moveSpeed = mSpeed;
    }
    
    void Start () {
        physics = GetComponent<Rigidbody>();
        //physics.velocity = new Vector3(10, 0, 10);
        interpolator = new Linear(cPoints, closedLoop);
        //interpolator = new BSpline (cPoints, closedLoop);
        //interpolator = new CatmullRom(cPoints, closedLoop);
        u = 0.0f;
        myMaterial = GetComponent<Renderer>().material;
    }

    void Update() {
        u += Time.deltaTime * moveSpeed;
        
        if (u >= interpolator.length)
        {
            if (interpolator.closed)
                u -= interpolator.length;
            else
                u = interpolator.length;
        }

        Vector3 targetDirection = interpolator.Heading (u);

        transform.position = interpolator.Evaluate (u);

        float singleStep = turnSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, 
				targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        physics.velocity = transform.forward * turnSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSensor : SphereSensor
{
    public float angle;
    private float minCosine;

    // Start is called before the first frame update
    void Start()
    {
        SetRadius(radius);
        SetAngle(angle);
    }

    public void SetAngle(float degrees) 
    {
        angle = degrees * Mathf.Deg2Rad;
        minCosine = Mathf.Cos(angle);
    }

    override public bool CanSee(Transform target) {
        Vector3 delta = target.position - transform.position;

        if (delta.sqrMagnitude <= radius2 && Vector3.Dot(transform.forward, delta.normalized) >= minCosine) 
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit; 
            if (Physics.Raycast(transform.position, fwd, out hit, radius))
                return hit.transform != target;
        }
        return false;
    }
}

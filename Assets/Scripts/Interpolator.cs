using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interpolator
{
    public Vector3[] control;
    public bool closed;
    public int length;

    protected int lengthM1;

    public Interpolator (Vector3[] points, bool closed) {
        SetControl(points);
        SetClosed(closed);
    }
    public Interpolator (Transform[] points, bool closed) {
        SetControl (points);
        SetClosed (closed);
    }

    public void SetControl (Vector3[] points) {
        control = points;
    }

    public void SetControl (Transform[] points) {
        length = points.Length;
        lengthM1 = length - 1;

        control = new Vector3[length];
        for (int i=0;i < length; i++)
            control[i] = points[i].position;
    }

    public void SetClosed (bool newClosed) {
        closed = newClosed;
    }

    public int Limit(int index) {
        if (index >= length) {
            if (closed)
                return index - length;
            return lengthM1;
        }
        return index;
    }

    public abstract Vector3 Evaluate (float u);
    public abstract Vector3 Heading (float u);
}

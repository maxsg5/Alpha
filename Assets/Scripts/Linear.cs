using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear: Interpolator {
    public Linear (Vector3[] pts, bool closed=false):base (pts, closed) {
        SetControl (pts);
        SetClosed (closed);
    }

    public Linear (Transform[] pts, bool closed=false):base (pts, closed) {
        SetControl (pts);
        SetClosed (closed);
    }

    public override Vector3 Evaluate (float u) {
        float dU;
        int startIndex, finishIndex;

        startIndex = Mathf.FloorToInt(u);
        CalculateFactors (u, closed,
                        out dU, ref startIndex, out finishIndex);
        return Vector3.Lerp (control[startIndex], control[finishIndex], dU);
    }

    public override Vector3 Heading (float u) {
        float dU;
        int startIndex, finishIndex;

        startIndex = Mathf.FloorToInt(u);
        CalculateFactors (u, closed,
                        out dU, ref startIndex, out finishIndex);
        return (control[finishIndex] - control[startIndex]).normalized;
    }

    private void CalculateFactors (float u, bool closed,
                out float dU, ref int startIndex, out int finishIndex) {
        startIndex = Mathf.FloorToInt (u);
        dU = u - startIndex;
        finishIndex = startIndex + 1;
       
       if (startIndex == lengthM1) {
           if (!closed) {
               finishIndex = startIndex;
               startIndex = startIndex - 1;
               dU += 1.0f;
           } else {
               finishIndex = 0;
           }
       }
    }
}
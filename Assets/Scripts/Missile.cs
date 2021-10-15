using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for missile tracking
/// Field           Description
/// target          Target to be tracked by missile
/// speed           Velocity of the Missile
/// rotationSpeed   Speed at which the Missile rotates to face target
/// rotateBy        Amount to rotate the Missile by
/// </summary>
///
/// Date        Author     Description
/// 2021-10-13  LS         Initial Testing
///
[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{

    public Transform target;
    new private Rigidbody2D rigidbody;
    public float speed = 2f;
    public float rotationSpeed = 100f;

    void Start()
    {
      // target = GameObject.FindGameObjectWithTag("Player").transform;
      rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
      Vector2 direction = (Vector2) target.position - rigidbody.position;
      direction.Normalize();

      float rotateBy = Vector3.Cross(direction, transform.up).z;
      rigidbody.angularVelocity = -rotateBy * rotationSpeed;
      rigidbody.velocity = transform.up * speed;
    }
}

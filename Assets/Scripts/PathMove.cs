using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the object to move along a set path using control points
/// set in the world
/// </summary>
/// 
/// Author: Josh Coss   (JC)
/// 
/// Variables
/// enemyPhysics    Rigidbody2D of enemy attached to pathmove
/// facingRight     bool, true if object is facing right
/// cpoints         List of transform points
/// startPos        float, x value of first point in cPoints
/// endPos          float, x value of last point in cPoints
/// enemySpeed      float, speed of enemy attached to pathmove
/// moveRight       bool, true if object is moving right
public class PathMove : MonoBehaviour
{
    Rigidbody2D enemyPhysics;
    
    public bool facingRight;
    public Transform[] cpoints = new Transform[2];

    private float startPos, endPos;

    private float enemySpeed;
    public bool moveRight = true;

    /// <summary>
    /// Stores the objects Rigidbody2D, startPos and endPos of cPoints, and determines
    /// if object is facing to the right
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Rewrote to better take advantage of 2D and gravity
    public void Start() {
        enemyPhysics = GetComponent<Rigidbody2D>();
        facingRight = transform.localScale.x > 0;
        if (facingRight == true) {
            startPos = cpoints[0].position.x;
            endPos = cpoints[1].position.x;
        } else {
            startPos = cpoints[1].position.x;
            endPos = cpoints[0].position.x;
        }
    }

    /// <summary>
    /// Updates basic movement left and right. 
    /// If moveRight is true, enemy will move to the right. If it is not 
    /// facing right, object will flip horizontally.
    /// If moveRight is false, enemy will move to the left. If it is not
    /// facing left, object will flip horizontally.
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    /// 2021-10-14  JC          Rewrote to better take advantage of 2D and gravity
    public void FixedUpdate() {
        if (moveRight) {
            enemyPhysics.position += Vector2.right * enemySpeed * Time.deltaTime;
            if (!facingRight) {
                flip();
            }
        }

        if (enemyPhysics.position.x >= endPos) {
            moveRight = false;
        }

        if (!moveRight) {
            enemyPhysics.position += -Vector2.right * enemySpeed * Time.deltaTime;
            if (facingRight) {
                flip();
            }
        }
        
        if (enemyPhysics.position.x <= startPos) {
            moveRight = true;
        }
    }

    /// <summary>
    /// Flips the object and updates facingRight 
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-14  JC          Initial Testing
    public void flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = transform.localScale.x > 0;
    }

    /// <summary>
    /// Flips the object and updates facingRight 
    /// </summary>
    /// 
    /// Date        Author      Description
    /// 2021-10-13  JC          Initial Testing
    public void setMoveSpeed(float mSpeed) {
        enemySpeed = mSpeed;
    }

    public void stop() {
        enemySpeed = 0f;
    }

}

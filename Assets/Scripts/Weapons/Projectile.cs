using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
	public float speed = 5.0f;
	
	[SerializeField] private float despawn_timer = 3.0f;
	private Vector2 direction;

    private void Start()
    {
        Destroy(this.gameObject, this.despawn_timer);
        Vector3 right = this.transform.right;
        this.direction = new Vector2(right.x, right.y);
        Vector3 velocity = this.direction * this.speed;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
	    Destroy(this.gameObject);
    }
}

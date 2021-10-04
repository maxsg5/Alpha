using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 5.0f;
	
	[SerializeField] private float despawn_timer = 3.0f;
	private Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(this.gameObject, this.despawn_timer);
    }

    void Update()
    {
        this.gameObject.transform.Translate(this.speed * Time.deltaTime * this.direction);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
	    Destroy(this.gameObject);
    }
}

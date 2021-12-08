// Author: Declan Simkins

using UnityEngine;

/// <summary>
/// Creates a simple projectile which travels until it reaches its
/// end of life timer or it collides with an object
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private float despawn_timer = 3.0f;
	private Vector2 direction;
	
	public float speed = 5.0f;

	/// <summary>
	/// Sets the projectiles direction and velocity
	/// </summary>
    private void Start()
    {
        Destroy(this.gameObject, this.despawn_timer);
        
        Vector3 right = this.transform.right;
        this.direction = new Vector2(right.x, right.y);
        
        Vector3 velocity = this.direction * this.speed;
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

	/// <summary>
	/// Destroys this projectile if the other object is a non-trigger collider
	/// </summary>
	/// <param name="other">Other collider involved in the collision</param>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.isTrigger) {
		    return;
	    }
	    Destroy(this.gameObject);
    }
}

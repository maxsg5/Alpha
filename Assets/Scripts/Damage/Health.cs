// Author: Declan Simkins

using UnityEngine;

/// <summary>
/// Tracks health and invokes events to reflect changes
/// in the health value
/// </summary>
public class Health : MonoBehaviour
{
	public float max_health = 100.0f;
	public float health = 100.0f;

	public delegate void On_Health_Changed(float new_health);
	public event On_Health_Changed Health_Changed;

	public delegate void On_Death(GameObject obj);
	public event On_Death Death;

	/// <summary>
	/// Invoke health changed event
	/// </summary>
	private void Start()
	{
		this.Health_Changed?.Invoke(this.max_health);
	}

	/// <summary>
	/// Add health, not passing max health
	/// Invokes Health_Changed event with the new current health value
	/// </summary>
	/// <param name="amount">Amount of health to add</param>
    public void Add_Health(float amount)
    {
        this.health += amount;
        if (this.health > this.max_health)
        {
            this.health = this.max_health;
        }
		this.Health_Changed?.Invoke(this.health);
    }

	/// <summary>
	/// Reduces current health
	/// Invokes Health_Changed event
	/// </summary>
	/// <param name="damage">Amount of health to remove</param>
    public virtual void Take_Damage(float damage)
	{
		this.health -= damage;
		this.Health_Changed?.Invoke(this.health);
		
		if (this.health <= 0) {
			this.health = 0;
			this.No_Health();
		}
		else if (this.health > this.max_health) {
			this.health = this.max_health;
		}
	}

	/// <summary>
	/// Invokes death event if this game object is the player
	/// Otherwise simply destroys the game object
	/// </summary>
    protected virtual void No_Health()
    {
		if (this.gameObject.tag == "Player") {
			this.Death?.Invoke(this.gameObject);
			Destroy(this.gameObject);
		}else if (this.gameObject.tag == "Destructable")
        {
			Destroy(this.gameObject);
        }
    } 
}

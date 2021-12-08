// Author: Declan Simkins

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Creates a line which projects outwards until it collides with something
/// or would reach a fixed max length 
/// </summary>
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Projectile_Beam : MonoBehaviour
{
	private LineRenderer beam_lr;
	private EdgeCollider2D beam_collider;
	private GameObject origin_obj;
	private Vector2 origin_pos;
	private Camera main_camera;
	private GameObject player;

	[SerializeField] private float beam_width = 0.25f;
	[SerializeField] private float max_length = 50.0f;

	/// <summary>
	/// Grabs necessary components and sets up the line renderer
	/// </summary>
	private void Awake()
	{
		this.beam_lr = this.GetComponent<LineRenderer>();
		this.beam_collider = this.GetComponent<EdgeCollider2D>();
		this.beam_lr.startWidth = this.beam_width;
		this.beam_lr.endWidth = this.beam_width;
		this.player = GameObject.FindWithTag("Player");
	}

	/// <summary>
	/// Projects and updates the line
	/// </summary>
	private void LateUpdate()
	{
		this.Project_Line();
	}

	/// <summary>
	/// Performs a raycast to determine the true collision point of the line
	/// Uses this to set the points for the edge collider and the
	/// line renderer
	/// </summary>
	private void Project_Line()
	{
		this.origin_pos = this.origin_obj.transform.position;
		RaycastHit2D[] hits = Physics2D.RaycastAll(
			this.origin_pos
			, this.origin_obj.transform.right
		);
		
		List<GameObject> ignorables = new List<GameObject>()
		{
			this.player,
			this.gameObject,
			this.origin_obj
		};
		
		// Find collision point or create one if there is no collision point
		Vector2 collision_point;
		int real_hit_i = this.Find_First_Hit_Index(hits, ignorables);
		if (hits.Length == 0 || real_hit_i < 0) {
			Vector2 origin_right = this.origin_obj.transform.right;
			Vector2 origin_right_scaled = origin_right * this.max_length;
			collision_point = this.origin_pos + origin_right_scaled;
		}
		else {
			collision_point = hits[real_hit_i].point;
		}
		
		// Set line renderer points
		this.beam_lr.SetPosition(0, this.origin_pos);
		this.beam_lr.SetPosition(1, collision_point);
		
		// Set edge collider points
		Vector2 collider_local_origin_pos = this.origin_obj.transform
			.InverseTransformPoint(this.origin_pos);
		Vector2 collider_local_collision_point = this.origin_obj.transform
			.InverseTransformPoint(collision_point);
		this.beam_collider.points = new[]
		{
			collider_local_origin_pos,
			collider_local_collision_point
		};
	}

	/// <summary>
	/// Finds the first "real" hit from a given list of raycast hits; this
	/// ignores anything in the `ignorable_objs` list and any triggers
	/// </summary>
	/// 
	/// <param name="hits">Raycast hits to be examined</param>
	/// <param name="ignorable_objs">
	/// Objects to be removed from consideration as "real" hits
	/// </param>
	/// 
	/// <returns>The index of the first "real" hit</returns>
	private int Find_First_Hit_Index(RaycastHit2D[] hits, List<GameObject> ignorable_objs)
	{
		int hit_i;
		bool ignorable_hit;
		for (hit_i = 0, ignorable_hit = true; ignorable_hit && hit_i < hits.Length; hit_i++) {
			GameObject hit_obj = hits[hit_i].transform.gameObject;
			ignorable_hit = ignorable_objs.Contains(hit_obj) || hit_obj.GetComponent<Collider2D>().isTrigger;
		}

		if (ignorable_hit) {
			return -1;
		}
		return hit_i - 1; // -1 because it increments before checking the condition on the last iteration of the loop
	}
	
	/// <summary>
	/// Used to instantiate a beam.
	/// </summary>
	/// 
	/// <param name="prefab">Prefab of the beam projectile</param>
	/// <param name="origin_obj">
	/// Transform which is instantiating the beam; this transform will be ignored
	/// when raycasting to find the beam's collision point
	/// </param>
	/// 
	/// <returns>
	/// GameObject with the `Projectile_Beam` script attached as well
	/// as any other necessary scripts
	/// </returns>
	public static GameObject Create_Beam(GameObject prefab, Transform origin_obj)
	{
		Vector2 origin_pos = origin_obj.position; 
		GameObject beam = Instantiate(prefab, origin_obj);
		Projectile_Beam beam_projectile = beam.GetComponent<Projectile_Beam>();

		// Adding projectile beam will add all other required components if
		// they are not already present
		if (beam_projectile == null) {
			beam_projectile = beam.AddComponent<Projectile_Beam>();
		}
		
		// TODO: Check for required components and add them if necessary
		//   this is necessary because if the Projectile_Beam script is already
		//   present, but the other required components are not, they will not
		//   be added but will be assumed to exist

		beam_projectile.origin_obj = origin_obj.gameObject;
		beam_projectile.origin_pos = origin_pos;

		return beam;
	}
}

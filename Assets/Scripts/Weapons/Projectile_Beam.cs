using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Projectile_Beam : MonoBehaviour
{
	private LineRenderer beam_lr;
	private EdgeCollider2D beam_collider;
	private GameObject origin_obj;
	private Vector2 origin_pos;
	private Camera main_camera;

	[SerializeField] private float beam_width = 0.25f;
	[SerializeField] private float max_length = 50.0f;

	private void Awake()
	{
		this.beam_lr = this.GetComponent<LineRenderer>();
		this.beam_collider = this.GetComponent<EdgeCollider2D>();
		this.beam_lr.startWidth = this.beam_width;
		this.beam_lr.endWidth = this.beam_width;
		this.main_camera = Camera.main;
	}

	private void LateUpdate()
	{
		this.origin_pos = this.origin_obj.transform.position;
		this.beam_lr.SetPosition(0, this.origin_pos);
		
		RaycastHit2D[] hits = Physics2D.RaycastAll(
			this.origin_pos
			, this.origin_obj.transform.right
		);
		
		Vector2 collision_point;
		List<GameObject> ignorables = new List<GameObject>()
		{
			GameObject.FindWithTag("Player"),
			this.gameObject,
			this.origin_obj
		};
		int real_hit_i = this.Find_First_Hit_Index(hits, ignorables);
		if (hits.Length == 0 || real_hit_i < 0) {
			Vector3 mouse_screen_pos = Input.mousePosition;
			mouse_screen_pos.z = 0;
			Vector2 mouse_pos_2d = this.main_camera.ScreenToWorldPoint(mouse_screen_pos);
			collision_point = (mouse_pos_2d - this.origin_pos).normalized * this.max_length;
		}
		else {
			collision_point = hits[real_hit_i].point;
		}
		
		this.beam_lr.SetPosition(1, collision_point);
		Vector2 collider_local_origin_pos = this.origin_obj.transform.InverseTransformPoint(this.origin_pos);
		Vector2 collider_local_collision_point = this.origin_obj.transform.InverseTransformPoint(collision_point);
		this.beam_collider.points = new[] {collider_local_origin_pos , collider_local_collision_point};
	}

	private int Find_First_Hit_Index(RaycastHit2D[] hits, List<GameObject> ignorable_objs)
	{
		int hit_i;
		bool ignorable_hit;
		for (hit_i = 0, ignorable_hit = true; ignorable_hit && hit_i < hits.Length; hit_i++) {
			GameObject hit_obj = hits[hit_i].transform.gameObject;
			ignorable_hit = ignorable_objs.Contains(hit_obj);
		}

		if (ignorable_hit) {
			return -1;
		}
		return hit_i - 1; // -1 because it increments before checking the condition on the last iteration of the loop
	}
	
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
		//   this is necessary because i the Projectile_Beam script is already
		//   present, but the other required components are not, they will not
		//   be added but will be assumed to exist

		beam_projectile.origin_obj = origin_obj.gameObject;
		beam_projectile.origin_pos = origin_pos;

		return beam;
	}
}

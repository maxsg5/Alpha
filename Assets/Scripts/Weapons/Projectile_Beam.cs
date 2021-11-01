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

	[SerializeField] private float beam_width = 0.25f;

	private void Awake()
	{
		this.beam_lr = this.GetComponent<LineRenderer>();
		this.beam_collider = this.GetComponent<EdgeCollider2D>();
		this.beam_lr.startWidth = this.beam_width;
		this.beam_lr.endWidth = this.beam_width;
	}

	private void LateUpdate()
	{
		this.origin_pos = this.origin_obj.transform.position;
		this.beam_lr.SetPosition(0, this.origin_pos);
		RaycastHit2D[] hits = Physics2D.RaycastAll(
			this.origin_pos
			, this.origin_obj.transform.right
		);
		
		int first_real_hit_i = 0;
		foreach (RaycastHit2D hit in hits) {
			GameObject obj = hit.transform.gameObject;
			if (obj.CompareTag("Player")
			|| obj == this.gameObject
			|| obj == this.origin_obj) {
				first_real_hit_i++;
			}
		}
		RaycastHit2D real_hit = hits[first_real_hit_i];

		Vector2 collision_point;
		if (real_hit.transform != null) {
			collision_point = real_hit.point;
		}
		else {
			collision_point = this.origin_pos;
		}
		
		this.beam_lr.SetPosition(1, collision_point);
		this.beam_collider.points = new[] {(Vector2) this.origin_pos, collision_point};
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

		beam_projectile.origin_obj = origin_obj.gameObject;
		beam_projectile.origin_pos = origin_pos;

		return beam;
	}
}
